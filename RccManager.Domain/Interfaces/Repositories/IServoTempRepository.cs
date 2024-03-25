using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IServoTempRepository : IRepository<ServoTemp>
    {
        Task<IEnumerable<ServoTemp>> GetAll(Guid grupoOracaoId);
        bool ValidateServoTemp(string name, DateTime birthday, string cpf, string email, string cellPhone);
        Task<ServoTemp> GetById(Guid id);
        Task<ServoTemp> GetByNameCpfEmail(string name, string cpf, string email);

    }
}

