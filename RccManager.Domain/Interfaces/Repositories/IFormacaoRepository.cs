using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories;

public interface IFormacaoRepository : IRepository<Formacao>
{
    Task<IEnumerable<Formacao>> GetAll(bool active);
    Task<bool> GetByName(string name);
    Task<bool> GetByName(string name, Guid id);
}

