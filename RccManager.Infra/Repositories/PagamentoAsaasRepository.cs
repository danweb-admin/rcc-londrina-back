using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
  public class PagamentoAsaasRepository : BaseRepository<PagamentosAsaas>,  IPagamentoAsaasRepository
  {
    private readonly DbSet<PagamentosAsaas> dbSet;


    public PagamentoAsaasRepository(AppDbContext _context) : base(_context)
    {
        dbSet = _context.Set<PagamentosAsaas>();
    }

    public async Task<PagamentosAsaas> AddAsync(PagamentosAsaas p)
    {
      try
      {
        dbSet.Add(p);
        await context.SaveChangesAsync();
        return p;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw;
      }
        
    }

    public Task<bool> Delete(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<PagamentoAsaas>> GetAll(string search)
    {
      throw new NotImplementedException();
    }

    public async Task<PagamentosAsaas> GetByAsaasIdAsync(string asaasId)
    {
      return await dbSet.FirstOrDefaultAsync(x => x.AsaasPaymentId == asaasId);

    }

    public Task<PagamentoAsaas> GetById(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<PagamentoAsaas> Insert(PagamentoAsaas entity)
    {
      throw new NotImplementedException();
    }

    public Task<IQueryable<PagamentoAsaas>> Query(Expression<Func<PagamentoAsaas, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<PagamentoAsaas> Update(PagamentoAsaas entity)
    {
      throw new NotImplementedException();
    }

    public async Task UpdateAsync(PagamentosAsaas p)
    {
        dbSet.Update(p);
        await context.SaveChangesAsync();
    }
  }
}

