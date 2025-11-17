using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories;

public interface IDecanatoSetorRepository : IRepository<DecanatoSetor>
{
    Task<bool> GetByName(string name);
    Task<bool> GetByName(string name,Guid id);
    Task<DecanatoSetor> GetById(Guid id);

}

