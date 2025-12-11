using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
  public interface IPagamentoAsaasRepository : IRepository<PagamentoAsaas>
  {
    Task<PagamentosAsaas> AddAsync(PagamentosAsaas p);
    Task<PagamentosAsaas> GetByAsaasIdAsync(string asaasId);
    Task UpdateAsync(PagamentosAsaas p);
  }
}

