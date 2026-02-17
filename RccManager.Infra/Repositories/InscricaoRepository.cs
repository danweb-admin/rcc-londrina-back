using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;
using System.Linq;

namespace RccManager.Infra.Repositories
{
    public class InscricaoRepository : BaseRepository<Inscricao>, IInscricaoRepository
    {
        private readonly DbSet<Inscricao> dbSet;

        public InscricaoRepository(AppDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Inscricao>();
        }

        public async Task<bool> AnyInscricao(string codigoInscricao)
        {
            return await dbSet.AnyAsync(x => x.CodigoInscricao == codigoInscricao);
        }

        public async Task<Inscricao> CheckByCpf(Guid eventId, string cpf)
        {
            
            return await dbSet.Where(i => i.Cpf == cpf && i.EventoId == eventId)
                    .OrderByDescending(i => i.Id)
                    .FirstOrDefaultAsync();
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

        public async Task<Inscricao> GetByInscricao(string codigoInscricao)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.CodigoInscricao == codigoInscricao);

        }

        public async Task InsertCamposDinamicos(InscricaoCampoValores camposDinamicos)
        {
            try
            {
                
                context.InscricaoCampoValores.Add(camposDinamicos);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

