using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class GrupoOracaoRepository : BaseRepository<GrupoOracao>, IGrupoOracaoRepository
    {
        private DbSet<GrupoOracao> dbSet;

        public GrupoOracaoRepository(AppDbContext _context) : base(_context)
        {
            dbSet = context.Set<GrupoOracao>();
        }

        public new async Task<GrupoOracao> GetById(Guid id)
        {

            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public new async Task<IEnumerable<GrupoOracao>> GetAll(string search)
        {
            search = search.ToUpper();

            return await dbSet
                .Include("ParoquiaCapela")
                .Include("ServosTemp")
                .Include(x => x.ParoquiaCapela.DecanatoSetor)
                .Where(
                    x => x.Name.Contains(search) ||
                    x.Address.Contains(search) ||
                    x.Neighborhood.Contains(search))
                .OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<GrupoOracao>> GetAll(string search, User user)
        {
            search = search.ToUpper();

            IQueryable<GrupoOracao> query = dbSet
              .Include(x => x.ParoquiaCapela)
              .ThenInclude(x => x.DecanatoSetor)
              .Include(x => x.Servos)
              .Include(x => x.ServosTemp);

            // 🔥 Filtro por Decanato (se existir)
            if (user.DecanatoSetorId.HasValue)
            {
              query = query.Where(x =>
                  x.ParoquiaCapela.DecanatoId == user.DecanatoSetorId);
            }

            // 🔥 Filtro por Grupo de Oração (se existir)
            if (user.GrupoOracaoId.HasValue)
            {
              query = query.Where(x => x.Id == user.GrupoOracaoId);
            }

            // 🔥 Filtro de busca
            if (!string.IsNullOrEmpty(search))
            {
              query = query.Where(x =>
                  x.Name.ToUpper().Contains(search) ||
                  x.Address.ToUpper().Contains(search) ||
                  x.Neighborhood.ToUpper().Contains(search) ||
                  x.ParoquiaCapela.DecanatoSetor.Name.ToUpper().Contains(search));
            }

            // 🔥 Ordenação e execução final
            return await query
                .OrderBy(x => x.Name)
                .ToListAsync();
        }


        public GrupoOracao GetByName(string name, string paroquiaCapelaName)
        {
            return dbSet.FirstOrDefault(x => x.Name == name && x.ParoquiaCapela.Name == paroquiaCapelaName);
        }

        public async Task<IEnumerable<GrupoOracao>> GetAll()
        {
            return await dbSet
                .Include(x => x.ParoquiaCapela)
                .Include(x => x.ParoquiaCapela.DecanatoSetor).ToListAsync();
        }
    }
}

