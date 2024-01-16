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

            var sql = await dbSet
                .Include("ParoquiaCapela")
                .Include("Servos")
                .Include("ServosTemp")
                .Include(x => x.ParoquiaCapela.DecanatoSetor).ToListAsync();

            if (!user.DecanatoSetorId.HasValue && !user.GrupoOracaoId.HasValue)
            {
                return sql.Where(
                    x => x.Name.Contains(search) ||
                    x.Address.Contains(search) ||
                    x.Neighborhood.Contains(search))
                    .OrderBy(x => x.Name).ToList();
            }

            if (user.DecanatoSetorId.HasValue)
            {
                sql = sql.Where(x => 
                            x.ParoquiaCapela.DecanatoId == user.DecanatoSetorId)
                         .Where(
                            x => x.Name.Contains(search) ||
                            x.Address.Contains(search) ||
                            x.Neighborhood.Contains(search))
                         .ToList();
            }


            if (user.GrupoOracaoId.HasValue)
            {
                sql = sql.Where(x =>
                        x.Id == user.GrupoOracaoId).ToList();
            }

            return sql.OrderBy(x => x.Name);

        }

        public async Task<GrupoOracao> GetByName(string name)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}

