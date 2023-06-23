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


}

