using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories;

public class ServoRepository : BaseRepository<Servo>, IServoRepository
{
    private readonly DbSet<Servo> dbSet;

    public ServoRepository(AppDbContext _context) : base(_context)
    {
        dbSet = _context.Set<Servo>();
    }

    public async Task<IEnumerable<Servo>> GetAll(Guid grupoOracaoId)
    {
        return await dbSet
        .Where(x => x.GrupoOracaoId == grupoOracaoId)
        .OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<bool> GetByCPF(string cpf)
    {
        return await dbSet.AnyAsync(x => x.Cpf.Equals(cpf));
    }

    public async Task Remove(Guid id)
    {
        var entity = await dbSet.FindAsync(id);
        dbSet.Remove(entity);

    }

    public async Task<bool> GetByCPF(Guid id, string cpf)
    {
        var a = await dbSet.Where(x => x.Cpf == cpf && x.Id != id).ToListAsync();
        return await dbSet.AnyAsync(x => x.Cpf == cpf && x.Id != id);
    }

    public async Task<bool> GetByEmail(string email)
    {
        return await dbSet.AnyAsync(x => x.Email.Equals(email));

    }

    public async Task<bool> GetByEmail(Guid id, string email)
    {
        return await dbSet.AnyAsync(x => x.Email == email && x.Id != id);

    }
}

