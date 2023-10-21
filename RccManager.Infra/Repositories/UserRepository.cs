using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbSet<User> dbSet;

    public UserRepository(AppDbContext _context) : base(_context)
    {
        dbSet = _context.Set<User>();
    }

    public new async Task<IEnumerable<User>> GetAll(string search)
    {
        search = search.ToUpper();

        return await dbSet
            .Include("DecanatoSetor")
            .Include("GrupoOracao")
            .Where(
                x => x.Name.Contains(search) ||
                x.Email.Contains(search.ToLower()) ||
                x.DecanatoSetor.Name.Contains(search))
            .OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<User> GetByName(string name)
    {
        return await dbSet.FirstAsync(x => x.Name.Equals(name));
    }
}

