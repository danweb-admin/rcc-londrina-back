using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IHistoryRepository : IRepository<History>
	{
        Task<IEnumerable<History>> GetAll(string tableName, Guid recordId);
        Task Add(string tableName, Guid recordId, string operation);
    }
}

