using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IInscricoesEventoRepository : IRepository<PagamentoAsaas>
    {
        Task<bool> CheckByCpf(Guid eventId, string cpf);

    }
}

