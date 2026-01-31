using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Helpers;
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
            var evento = await dbSet
                .AsTracking()
                .Include(x => x.Local)
                .Include(x => x.Sobre)
                .Include(x => x.InformacoesAdicionais)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (evento == null) return null;

            await context.Entry(evento).Collection(x => x.LotesInscricoes).LoadAsync();
            await context.Entry(evento).Collection(x => x.Programacao).LoadAsync();
            await context.Entry(evento).Collection(x => x.Participacoes).LoadAsync();
            await context.Entry(evento).Collection(x => x.Inscricoes).LoadAsync();

            return evento;
        }



        public async Task<string> GetSlug(Guid id)
        {
            var evento = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (evento == null)
              return "sem-grupo";

            return evento.Slug;

        }

        public async Task<Evento> GetSlug(string slug)
        {
            return await dbSet
                .Include(x => x.Local)
                .Include(x => x.Sobre)
                .Include(x => x.InformacoesAdicionais)
                .Include(x => x.LotesInscricoes)
                .Include(x => x.Programacao)
                .Include(x => x.Participacoes)
                .Include(x => x.Participacoes)
                .FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<Evento> Update(Evento entity)
        {
            var result = await dbSet
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (result == null)
                return null;

            // Preserva datas
            entity.CreatedAt = result.CreatedAt;
            entity.UpdatedAt = Helpers.DateTimeNow();

            // Atualiza propriedades escalares
            context.Entry(result).CurrentValues.SetValues(entity);

            // 🔒 BLOQUEIA APENAS A COLEÇÃO INSCRIÇÕES
            var inscricoesNav = context.Entry(result).Navigation("Inscricoes");
            if (inscricoesNav != null)
            {
                inscricoesNav.IsModified = false;
            }

            await context.SaveChangesAsync();

            return result;
        }

        public Task<IEnumerable<Evento>> GetAΩll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Evento>> GetEventsByEmail(string email)
        {
            var events = context.UsuariosCheckin.Where(x => x.Email == email).Select(x => x.EventoId);

            return await dbSet.Where(x => events.Contains(x.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Inscricao>> GetAllInscricoesByEvento(Guid eventoId)
        {
            var statuts_ = new List<string> { "isento","pagamento_confirmado"};
            return await context.Inscricoes.Where(x => x.EventoId == eventoId && statuts_.Contains(x.Status) ).ToListAsync();
        }
    }
}
