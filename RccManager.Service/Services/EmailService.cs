using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Helper;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace RccManager.Service.Services
{
	public class EmailService : IEmailService
	{
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarEmailPagamentoConfirmado(Inscricao inscricao)
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
                    .Replace("{{VALOR}}",$"R$ {inscricao.ValorInscricao.ToString().Replace(".",",")}")
                    .Replace("{{NOME_EVENTO}}", inscricao.Evento.Nome)
                    .Replace("{{DATA_INICIAL}}", inscricao.Evento.DataInicio.ToString("dd/MM/yyyy"))
                    .Replace("{{DATA_FINAL}}", inscricao.Evento.DataFim.ToString("dd/MM/yyyy"))
                    .Replace("{{LOCAL_EVENTO}}", formatarLocal(inscricao.Evento.Local))
                    .Replace("{{ORGANIZADOR}}", inscricao.Evento.OrganizadorNome)
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
                    Subject = $"Pagamento confirmado! Seu ingresso está disponível - {inscricao.Evento.Nome}",
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

        public async Task EnviarEmailPagamentoConfirmado(InscricaoMQResponse inscricao)
        {
            try
            {
                var smtpServer = Environment.GetEnvironmentVariable("SmtpServer");
                var porta = Environment.GetEnvironmentVariable("Port");
                var senderEmail = Environment.GetEnvironmentVariable("SenderEmail");
                var senderPassword = Environment.GetEnvironmentVariable("SenderPassword");

                // Validar variáveis de ambiente
                if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(porta) ||
                    string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
                {
                    throw new InvalidOperationException("Variáveis de ambiente SMTP não configuradas corretamente");
                }

                var nomeOrganizacao = "Renovação Carismática Católica Arquidiocese de Londrina";
                var logoUrl = "https://res.cloudinary.com/dgcpvxvcj/image/upload/v1763292856/Fotos%20Eventos/Rcc.jpg";

                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "email-confirmacao.html");
                
                if (!File.Exists(templatePath))
                {
                    throw new FileNotFoundException($"Template não encontrado: {templatePath}");
                }

                var valorTexto = inscricao.Status == "isento" ? "Isento" : $"R$ {inscricao.ValorInscricao:F2}".Replace(".", ",");

                Utils.GerarQrCodePNG(inscricao.CodigoInscricao);

                var urlQrCode = $"http://localhost:5290/qrcodes/{inscricao.CodigoInscricao}.png";

                string html = await File.ReadAllTextAsync(templatePath);

                html = html
                    .Replace("{{NOME}}", inscricao.Nome)
                    .Replace("{{EMAIL}}", inscricao.Email)
                    .Replace("{{CPF}}", inscricao.Cpf)
                    .Replace("{{CODIGO_INSCRICAO}}", inscricao.CodigoInscricao)
                    .Replace("{{VALOR}}", valorTexto)
                    .Replace("{{NOME_EVENTO}}", inscricao.NomeEvento)
                    .Replace("{{DATA_INICIAL}}", inscricao.DataInicio.ToString("dd/MM/yyyy"))
                    .Replace("{{DATA_FINAL}}", inscricao.DataFim.ToString("dd/MM/yyyy"))
                    .Replace("{{LOCAL_EVENTO}}", inscricao.Local)
                    .Replace("{{ORGANIZADOR}}", inscricao.OrganizadorNome)
                    .Replace("{{LOGO_URL}}", logoUrl)
                    .Replace("{{NOME_ORGANIZACAO}}", nomeOrganizacao)
                    .Replace("{{QRCODE_URL}}", urlQrCode);;
                    

                using var smtp = new SmtpClient(smtpServer, int.Parse(porta))
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    Timeout = 30000 // 30 segundos
                };

                var message = new MailMessage
                {
                    From = new MailAddress(senderEmail,"RCC Londrina - Cadastramento"),
                    Subject = $"Pagamento confirmado! Seu ingresso está disponível - {inscricao.NomeEvento} - {inscricao.CodigoInscricao}",
                    Body = html,
                    IsBodyHtml = true
                };

                message.To.Add(inscricao.Email);

                await smtp.SendMailAsync(message);
                
                Console.WriteLine($"📧 Email enviado com sucesso para {inscricao.Email}, {inscricao.Nome}");
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"❌ Erro SMTP: {smtpEx.Message}");
                Console.WriteLine($"Status Code: {smtpEx.StatusCode}");
                throw; // Re-throw para que a mensagem seja reprocessada
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao enviar email: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw; // Re-throw para que a mensagem seja reprocessada
            }
        }

        public async Task SendEmailAsync(string destinatario, string assunto, string mensagem)
        {
            var smtpServer = _config["EmailSettings:SmtpServer"];
            var porta = int.Parse(_config["EmailSettings:Port"]);
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var senderPassword = _config["EmailSettings:SenderPassword"];

            using (var client = new SmtpClient(smtpServer, porta))
            {
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = assunto,
                    IsBodyHtml = true,
                    Body = @"
                        <html>
                        <body style='font-family: Arial, sans-serif;'>
                            <h2 style='color: #2E86C1;'>Confirmação de Email</h2>
                            <p>Olá,</p>
                            <p>Você está recebendo este e-mail para confirmar o seu cadastro na Plataforma Gerenciador RCC! Clique no link abaixo para confirmar seu e-mail:</p>
                            <a href='http://161.35.255.131:31354/api/v1/user/confirmacao-email?email="  + destinatario + @"' style='background: #28a745; color: white; padding: 10px 15px; text-decoration: none;'>
                                Confirmar E-mail
                            </a>
                            <p>Se você não fez esse cadastro, ignore este e-mail.</p>
                        </body>
                        </html>"
                };

                mailMessage.To.Add(destinatario);

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private string formatarLocal(Local local)
        {
            var partes = new List<string>();

            if (!string.IsNullOrWhiteSpace(local.Endereco))
                partes.Add(local.Endereco);

            if (!string.IsNullOrWhiteSpace(local.Complemento))
                partes.Add(local.Complemento);

            if (!string.IsNullOrWhiteSpace(local.Bairro))
                partes.Add(local.Bairro);

            if (!string.IsNullOrWhiteSpace(local.Cidade))
                partes.Add(local.Cidade);

            if (!string.IsNullOrWhiteSpace(local.Estado))
                partes.Add(local.Estado);

            return string.Join(", ", partes);
        }
    }
}

