using System;
namespace RccManager.Domain.Interfaces.Services
{
	public interface IEmailService
	{
        Task SendEmailAsync(string to, string subject, string body);

    }
}

