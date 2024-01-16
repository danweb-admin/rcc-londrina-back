using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IServoTempRepository : IRepository<ServoTemp>
    {
        Task<IEnumerable<ServoTemp>> GetAll(Guid grupoOracaoId);
        Task<ServoTemp> GetById(Guid id);

    }
}

