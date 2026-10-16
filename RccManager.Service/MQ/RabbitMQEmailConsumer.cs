using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RccManager.Domain.Responses;
using RccManager.Service.Helper;

namespace RccManager.Service.MQ
{
    public class RabbitMQEmailConsumer : BackgroundService
    {
        private readonly RabbitMQConnection _rmq;
        private IChannel? _channel;

        private const string QueueName = "email_queue";

        public RabbitMQEmailConsumer(RabbitMQConnection rmq)
        {
            _rmq = rmq;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("🔌 Tentando conectar ao RabbitMQ...");

                    _channel = await _rmq.CreateChannelAsync();

                    Console.WriteLine(
                        "✅ Conectado ao RabbitMQ - Email com sucesso!"
                    );

                    await _channel.QueueDeclareAsync(
                        queue: QueueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    // Processar apenas uma mensagem por vez
                    await _channel.BasicQosAsync(
                        prefetchSize: 0,
                        prefetchCount: 1,
                        global: false
                    );

                    var consumer =
                        new AsyncEventingBasicConsumer(_channel);

                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        await ProcessarMensagem(ea, stoppingToken);
                    };

                    await _channel.BasicConsumeAsync(
                        queue: QueueName,
                        autoAck: false,
                        consumer: consumer
                    );

                    Console.WriteLine(
                        $"👂 Aguardando mensagens na fila '{QueueName}'..."
                    );

                    // Mantém o consumer ativo
                    await Task.Delay(
                        Timeout.Infinite,
                        stoppingToken
                    );
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine(
                        "🛑 Consumer está sendo encerrado..."
                    );

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "❌ ERRO NO CONSUMER RABBITMQ"
                    );

                    Console.WriteLine(ex.ToString());

                    if (!stoppingToken.IsCancellationRequested)
                    {
                        Console.WriteLine(
                            "⏳ Aguardando 5 segundos antes de reconectar..."
                        );

                        await Task.Delay(
                            TimeSpan.FromSeconds(5),
                            stoppingToken
                        );
                    }
                }
            }
        }

        private async Task ProcessarMensagem(
            BasicDeliverEventArgs ea,
            CancellationToken stoppingToken)
        {
            try
            {
                var body = ea.Body.ToArray();

                var json = Encoding.UTF8.GetString(body);

                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine(
                        "⚠️ Mensagem RabbitMQ vazia."
                    );

                    await RejeitarMensagem(ea, false);

                    return;
                }

                InscricaoMQResponse? inscricao;

                try
                {
                    inscricao =
                        JsonSerializer.Deserialize<InscricaoMQResponse>(
                            json
                        );
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(
                        "❌ Erro ao deserializar mensagem RabbitMQ."
                    );

                    Console.WriteLine(ex.ToString());

                    // Mensagem inválida não deve voltar para a fila
                    await RejeitarMensagem(ea, false);

                    return;
                }

                if (inscricao == null)
                {
                    Console.WriteLine(
                        "⚠️ Mensagem deserializada como null."
                    );

                    await RejeitarMensagem(ea, false);

                    return;
                }

                Console.WriteLine(
                    $"📩 Processando email para: {inscricao.Email}"
                );

                if (string.IsNullOrWhiteSpace(inscricao.Email))
                {
                    Console.WriteLine(
                        "⚠️ Inscrição sem email. Mensagem rejeitada."
                    );

                    await RejeitarMensagem(ea, false);

                    return;
                }

                await EnviarEmailPagamentoConfirmado(
                    inscricao,
                    stoppingToken
                );

                // Só confirma a mensagem depois que o email foi enviado
                await _channel!.BasicAckAsync(
                    ea.DeliveryTag,
                    multiple: false
                );

                Console.WriteLine(
                    $"✅ Email enviado com sucesso para: {inscricao.Email}"
                );
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(
                    "❌ ERRO SMTP AO PROCESSAR EMAIL"
                );

                Console.WriteLine(
                    $"Mensagem: {ex.Message}"
                );

                Console.WriteLine(
                    $"StatusCode: {ex.StatusCode}"
                );

                Console.WriteLine(
                    $"Exception: {ex}"
                );

                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        "❌ INNER EXCEPTION:"
                    );

                    Console.WriteLine(
                        ex.InnerException.ToString()
                    );
                }

                /*
                 * IMPORTANTE:
                 *
                 * Não usamos requeue=true aqui.
                 *
                 * Caso o SMTP esteja indisponível,
                 * a mensagem entraria em loop infinito:
                 *
                 * SMTP -> erro -> RabbitMQ -> SMTP -> erro...
                 *
                 * O ideal em produção é configurar uma
                 * Dead Letter Queue / Retry Queue.
                 */

                await RejeitarMensagem(ea, false);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "🛑 Processamento cancelado."
                );

                // Se a aplicação estiver sendo encerrada,
                // não tentamos confirmar a mensagem.
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "❌ ERRO AO PROCESSAR MENSAGEM"
                );

                Console.WriteLine(
                    ex.ToString()
                );

                /*
                 * Mensagem com erro inesperado.
                 * Por segurança não fazemos requeue infinito.
                 *
                 * Em produção o ideal é DLQ.
                 */

                await RejeitarMensagem(ea, false);
            }
        }

        private async Task EnviarEmailPagamentoConfirmado(
            InscricaoMQResponse inscricao,
            CancellationToken cancellationToken)
        {
            var smtpServer =
                Environment.GetEnvironmentVariable("SmtpServer");

            var porta =
                Environment.GetEnvironmentVariable("Port");

            var senderEmail =
                Environment.GetEnvironmentVariable("SenderEmail");

            var senderPassword =
                Environment.GetEnvironmentVariable("SenderPassword");

            ValidarConfiguracaoSmtp(
                smtpServer,
                porta,
                senderEmail,
                senderPassword
            );

            Console.WriteLine(
                $"📤 SMTP Server: {smtpServer}"
            );

            Console.WriteLine(
                $"📤 SMTP Port: {porta}"
            );

            Console.WriteLine(
                $"📤 SMTP Email: {senderEmail}"
            );

            Console.WriteLine(
                $"📤 SMTP Password configurada: " +
                $"{!string.IsNullOrWhiteSpace(senderPassword)}"
            );

            var nomeOrganizacao =
                "Renovação Carismática Católica Arquidiocese de Londrina";

            var logoUrl =
                "https://res.cloudinary.com/dgcpvxvcj/image/upload/v1763292856/Fotos%20Eventos/Rcc.jpg";

            var templatePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Templates",
                "email-confirmacao.html"
            );

            Console.WriteLine(
                $"📄 Template: {templatePath}"
            );

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException(
                    $"Template não encontrado: {templatePath}"
                );
            }

            var valorTexto =
                inscricao.Status == "isento"
                    ? "Isento"
                    : $"R$ {inscricao.ValorInscricao:F2}"
                        .Replace(".", ",");

            Utils.GerarQrCodePNG(
                inscricao.CodigoInscricao
            );

            var urlQrCode =
                $"https://backend.rcc-londrina.online/qrcodes/{inscricao.CodigoInscricao}.png";

            var html =
                await File.ReadAllTextAsync(
                    templatePath,
                    cancellationToken
                );

            html = html
                .Replace(
                    "{{NOME}}",
                    inscricao.Nome ?? string.Empty
                )
                .Replace(
                    "{{EMAIL}}",
                    inscricao.Email ?? string.Empty
                )
                .Replace(
                    "{{CPF}}",
                    inscricao.Cpf ?? string.Empty
                )
                .Replace(
                    "{{CODIGO_INSCRICAO}}",
                    inscricao.CodigoInscricao ?? string.Empty
                )
                .Replace(
                    "{{VALOR}}",
                    valorTexto
                )
                .Replace(
                    "{{NOME_EVENTO}}",
                    inscricao.NomeEvento ?? string.Empty
                )
                .Replace(
                    "{{DATA_INICIAL}}",
                    inscricao.DataInicio.ToString("dd/MM/yyyy")
                )
                .Replace(
                    "{{DATA_FINAL}}",
                    inscricao.DataFim.ToString("dd/MM/yyyy")
                )
                .Replace(
                    "{{LOCAL_EVENTO}}",
                    inscricao.Local ?? string.Empty
                )
                .Replace(
                    "{{ORGANIZADOR}}",
                    inscricao.OrganizadorNome ?? string.Empty
                )
                .Replace(
                    "{{LOGO_URL}}",
                    logoUrl
                )
                .Replace(
                    "{{NOME_ORGANIZACAO}}",
                    nomeOrganizacao
                )
                .Replace(
                    "{{QRCODE_URL}}",
                    urlQrCode
                );

            if (!int.TryParse(porta, out var smtpPort))
            {
                throw new InvalidOperationException(
                    $"Porta SMTP inválida: {porta}"
                );
            }

            using var smtp = new SmtpClient(
                smtpServer!,
                smtpPort
            )
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials =
                    new NetworkCredential(
                        senderEmail!,
                        senderPassword!
                    ),
                Timeout = 30000
            };

            using var message = new MailMessage
            {
                From = new MailAddress(
                    senderEmail!,
                    "RCC Londrina - Cadastramento"
                ),

                Subject =
                    $"Pagamento confirmado! " +
                    $"Seu ingresso está disponível - " +
                    $"{inscricao.NomeEvento} - " +
                    $"{inscricao.CodigoInscricao}",

                Body = html,

                IsBodyHtml = true
            };

            message.To.Add(
                new MailAddress(inscricao.Email!)
            );

            Console.WriteLine(
                $"📤 Enviando email para {inscricao.Email}..."
            );

            try
            {
                await smtp.SendMailAsync(message);

                Console.WriteLine(
                    $"📧 Email enviado com sucesso para " +
                    $"{inscricao.Email}, {inscricao.Nome}"
                );
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(
                    "❌ ERRO SMTP"
                );

                Console.WriteLine(
                    $"Message: {ex.Message}"
                );

                Console.WriteLine(
                    $"StatusCode: {ex.StatusCode}"
                );

                Console.WriteLine(
                    $"Type: {ex.GetType().FullName}"
                );

                Console.WriteLine(
                    $"Exception completa:"
                );

                Console.WriteLine(
                    ex.ToString()
                );

                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        "❌ INNER EXCEPTION:"
                    );

                    Console.WriteLine(
                        ex.InnerException.ToString()
                    );
                }

                throw;
            }
        }

        private static void ValidarConfiguracaoSmtp(
            string? smtpServer,
            string? porta,
            string? senderEmail,
            string? senderPassword)
        {
            if (string.IsNullOrWhiteSpace(smtpServer))
            {
                throw new InvalidOperationException(
                    "Variável SmtpServer não configurada."
                );
            }

            if (string.IsNullOrWhiteSpace(porta))
            {
                throw new InvalidOperationException(
                    "Variável Port não configurada."
                );
            }

            if (string.IsNullOrWhiteSpace(senderEmail))
            {
                throw new InvalidOperationException(
                    "Variável SenderEmail não configurada."
                );
            }

            if (string.IsNullOrWhiteSpace(senderPassword))
            {
                throw new InvalidOperationException(
                    "Variável SenderPassword não configurada."
                );
            }

            if (!int.TryParse(porta, out _))
            {
                throw new InvalidOperationException(
                    $"Porta SMTP inválida: {porta}"
                );
            }
        }

        private async Task RejeitarMensagem(
            BasicDeliverEventArgs ea,
            bool requeue)
        {
            try
            {
                if (_channel == null)
                    return;

                await _channel.BasicNackAsync(
                    ea.DeliveryTag,
                    multiple: false,
                    requeue: requeue
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "❌ Erro ao executar BasicNack:"
                );

                Console.WriteLine(
                    ex.ToString()
                );
            }
        }

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            Console.WriteLine(
                "🛑 Encerrando consumer de email..."
            );

            try
            {
                if (_channel != null)
                {
                    await _channel.CloseAsync();
                    _channel.Dispose();
                    _channel = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "⚠️ Erro ao fechar canal RabbitMQ:"
                );

                Console.WriteLine(
                    ex.ToString()
                );
            }

            await base.StopAsync(
                cancellationToken
            );
        }
    }
}
