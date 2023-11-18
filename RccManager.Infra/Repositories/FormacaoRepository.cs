using System;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories;

public class FormacaoRepository : BaseRepository<Formacao>, IFormacaoRepository
{
    private DbSet<Formacao> dbSet;

    public FormacaoRepository(AppDbContext _context) : base(_context)
    {
        dbSet = context.Set<Formacao>();
    }

    public async Task<bool> GetByName(string name)
    {
        return await dbSet.AnyAsync(x => x.Name.ToUpper() == name.ToUpper());
    }

    public async Task<bool> GetByName(string name, Guid id)
    {
        return await dbSet.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id);
    }

    public async Task<IEnumerable<Formacao>> GetAll()
    {
        return await dbSet.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<IEnumerable<Formacao>> GetAll(bool active)
    {
        return await dbSet.Where(x => x.Active == active).ToListAsync();
    }
}

