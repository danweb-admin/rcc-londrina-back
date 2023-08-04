using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories;

public class ParoquiaCapelaRepsoitory : BaseRepository<ParoquiaCapela>, IParoquiaCapelaRepository
{
    private readonly DbSet<ParoquiaCapela> dbSet;

    public ParoquiaCapelaRepsoitory(AppDbContext _context) : base(_context)
    {
        dbSet = _context.Set<ParoquiaCapela>();
    }

    public new async Task<IEnumerable<ParoquiaCapela>> GetAll()
    {
        return await dbSet.Include("DecanatoSetor").OrderBy(x => x.Name).ToListAsync();
    }
}

