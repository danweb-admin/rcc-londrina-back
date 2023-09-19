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

    public async Task<User> GetByEmail(string email)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<User> GetByName(string name)
    {
        return await dbSet.FirstAsync(x => x.Name.Equals(name));
    }
}

