using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class InscricaoRepository : BaseRepository<Inscricao>, IInscricaoRepository
    {
        private readonly DbSet<Inscricao> dbSet;

        public InscricaoRepository(AppDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Inscricao>();
        }

        public async Task<Inscricao> CheckByCpf(Guid eventId, string cpf)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Cpf.Equals(cpf) && x.EventoId.Equals(eventId));

        }

        public async Task<IEnumerable<Inscricao>> GetAll(Guid eventoId)
        {
            return await dbSet.Where(x => x.EventoId == eventoId).ToListAsync();
        }

        public async Task<Inscricao> GetByCodigo(string codigoInscricao)
        {
            return await dbSet.Include(x => x.Evento)
                .Include(x => x.Evento.Local)
                .FirstOrDefaultAsync(x => x.CodigoInscricao == codigoInscricao);
        }
    }
}

