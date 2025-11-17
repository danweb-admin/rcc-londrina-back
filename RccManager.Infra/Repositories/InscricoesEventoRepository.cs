using System;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
	public class InscricoesEventoRepository : BaseRepository<InscricoesEvento>, IInscricoesEventoRepository
    {
        private readonly DbSet<InscricoesEvento> dbSet;

        public InscricoesEventoRepository(AppDbContext _context) : base(_context)
        {
            dbSet = _context.Set<InscricoesEvento>();
        }

        public async Task<bool> CheckByCpf(Guid eventId, string cpf)
        {
            return await dbSet.AnyAsync(x => x.Cpf.Equals(cpf) && x.EventId.Equals(eventId));
        }
    }
}

