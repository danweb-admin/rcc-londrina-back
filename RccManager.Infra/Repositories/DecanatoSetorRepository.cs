using System;
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

    public async Task<bool> GetByName(string name)
    {
        return await dbSet.AnyAsync(x => x.Name.ToUpper() == name.ToUpper());
    }

    public async Task<bool> GetByName(string name, Guid id)
    {
        return await dbSet.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id);
    }
}

