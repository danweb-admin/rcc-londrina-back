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
                .Where(
                    x => x.Name.Contains(search) ||
                    x.Address.Contains(search) ||
                    x.Neighborhood.Contains(search))
                .OrderBy(x => x.Name).ToListAsync();
        }
    }
}

