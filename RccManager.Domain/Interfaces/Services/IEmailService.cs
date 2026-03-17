using System;
using RccManager.Domain.Entities;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
	public interface IEmailService
	{
        Task SendEmailAsync(string to, string subject, string body);

        Task EnviarEmailPagamentoConfirmado(Inscricao inscricao);

        Task EnviarEmailPagamentoConfirmado(InscricaoMQResponse inscricao);

    }
}

