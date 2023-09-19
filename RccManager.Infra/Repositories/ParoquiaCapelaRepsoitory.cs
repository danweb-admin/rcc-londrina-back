using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;
using StackExchange.Redis;

namespace RccManager.Infra.Repositories;

public class ParoquiaCapelaRepsoitory : BaseRepository<ParoquiaCapela>, IParoquiaCapelaRepository
{
    private readonly DbSet<ParoquiaCapela> dbSet;


    public ParoquiaCapelaRepsoitory(AppDbContext _context) : base(_context)
    {
        dbSet = _context.Set<ParoquiaCapela>();
    }

    public new async Task<IEnumerable<ParoquiaCapela>> GetAll(string search)
    {
        search = search.ToUpper();

        return await dbSet.Include("DecanatoSetor")
            .Where(
                x => x.Name.Contains(search) ||
                x.Address.Contains(search) ||
                x.Neighborhood.Contains(search) ||
                x.DecanatoSetor.Name.Contains(search))
            .OrderBy(x => x.Name).ToListAsync();
    }
}

