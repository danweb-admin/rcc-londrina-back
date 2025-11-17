using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IInscricoesEventoRepository : IRepository<InscricoesEvento>
    {
        Task<bool> CheckByCpf(Guid eventId, string cpf);

    }
}

