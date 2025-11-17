using System.Linq;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;


namespace RccManager.Infra.Repositories;

public class DecanatoSetorRepository : BaseRepository<DecanatoSetor>, IDecanatoSetorRepository
{
    private DbSet<DecanatoSetor> dbSet;

    public DecanatoSetorRepository(AppDbContext _context) : base(_context)
    {
        dbSet = context.Set<DecanatoSetor>();
    }

    public new async Task<DecanatoSetor> GetById(Guid id)
    {

        return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> GetByName(string name)
    {
        return await dbSet.AnyAsync(x => x.Name.ToUpper() == name.ToUpper());
    }

    public async Task<bool> GetByName(string name, Guid id)
    {
        return await dbSet.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id);
    }

    public async Task<IEnumerable<DecanatoSetor>> GetAll()
    {
        return await dbSet.OrderBy(x => x.Name).ToListAsync();
    }
}

