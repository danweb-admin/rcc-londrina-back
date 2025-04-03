using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using RccManager.Domain.Interfaces.Services;

namespace RccManager.Service.Services
{
	public class EmailService : IEmailService
	{
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
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
    }
}

