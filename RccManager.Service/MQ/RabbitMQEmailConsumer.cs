using System;
using System.Diagnostics.Tracing;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RccManager.Domain.Entities;
using RccManager.Domain.Responses;

namespace RccManager.Service.MQ
{
    public class RabbitMQEmailConsumer : BackgroundService
    {
        private readonly RabbitMQConnection _rmq;


        public RabbitMQEmailConsumer(RabbitMQConnection rmq)
        {
            _rmq = rmq;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var channel = await _rmq.CreateChannelAsync();

                await channel.QueueDeclareAsync("email_queue", durable: true, exclusive: false, autoDelete: false);

                var consumer = new AsyncEventingBasicConsumer(channel);

            
                consumer.ReceivedAsync += async (model, ea) =>
                {

                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    var message = JsonSerializer.Deserialize<InscricaoMQResponse>(json);

                    Console.WriteLine("📩 Enviar email: " + message.Email);

                    try
                    {
                        await EnviarEmailPagamentoConfirmado(message);
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        // devolve pra fila
                        await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                    }

                    
                };

                await channel.BasicConsumeAsync("email_queue", autoAck: false, consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            

            await Task.CompletedTask;
        }

        private async Task EnviarEmailPagamentoConfirmado(InscricaoMQResponse inscricao)
        {
            try
            {
                var smtpServer = Environment.GetEnvironmentVariable("SmtpServer");
                var porta = Environment.GetEnvironmentVariable("Port");
                var senderEmail = Environment.GetEnvironmentVariable("SenderEmail");
                var senderPassword = Environment.GetEnvironmentVariable("SenderPassword");

                var nomeOrganizacao = "Renovação Carismática Católica Arquidiocese de Londrina";
                var logoUrl = "https://res.cloudinary.com/dgcpvxvcj/image/upload/v1763292856/Fotos%20Eventos/Rcc.jpg";

                string html = File.ReadAllText("Templates/email-confirmacao.html");

                html = html
                    .Replace("{{NOME}}", inscricao.Nome)
                    .Replace("{{EMAIL}}", inscricao.Email)
                    .Replace("{{CPF}}", inscricao.Cpf)
                    .Replace("{{CODIGO_INSCRICAO}}", inscricao.CodigoInscricao)
                    .Replace("{{VALOR}}", $"R$ {inscricao.ValorInscricao.ToString().Replace(".", ",")}")
                    .Replace("{{NOME_EVENTO}}", inscricao.NomeEvento)
                    .Replace("{{DATA_INICIAL}}", inscricao.DataInicio.ToString("dd/MM/yyyy"))
                    .Replace("{{DATA_FINAL}}", inscricao.DataFim.ToString("dd/MM/yyyy"))
                    .Replace("{{LOCAL_EVENTO}}", inscricao.Local)
                    .Replace("{{ORGANIZADOR}}", inscricao.OrganizadorNome)
                    .Replace("{{LOGO_URL}}", logoUrl)
                    .Replace("{{NOME_ORGANIZACAO}}", nomeOrganizacao);

                using var smtp = new SmtpClient(smtpServer, int.Parse(porta))
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        senderEmail,
                        senderPassword
                    )
                };

                var message = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = $"Pagamento confirmado! Seu ingresso está disponível - {inscricao.NomeEvento}",
                    Body = html,
                    IsBodyHtml = true
                };

                message.To.Add(inscricao.Email);

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}

