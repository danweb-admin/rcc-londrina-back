using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        private readonly DbSet<Evento> dbSet;

        public EventoRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Evento>();
        }

        public async Task<IEnumerable<Evento>> GetAllHome()
        {
            return await dbSet
                .Where(x => x.Status == "Publicado")
                .OrderBy(x => x.Nome)
                .ToListAsync();
             
        }

        public async Task<IEnumerable<Evento>> GetAll()
        {
            return await dbSet
                .Include(x => x.Local)
                .Include(x => x.Sobre)
                .Include(x => x.InformacoesAdicionais)
                .Include(x => x.LotesInscricoes)
                .Include(x => x.Programacao)
                .Include(x => x.Participacoes)
                .Include(x => x.Participacoes)
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<Evento> GetById(Guid id)
        {
            return await dbSet
                .Include(x => x.Local)
                .Include(x => x.Sobre)
                .Include(x => x.InformacoesAdicionais)
                .Include(x => x.LotesInscricoes)
                .Include(x => x.Programacao)
                .Include(x => x.Participacoes)
                .Include(x => x.Inscricoes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
