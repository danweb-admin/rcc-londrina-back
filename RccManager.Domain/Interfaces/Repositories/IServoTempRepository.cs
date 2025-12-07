using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IServoTempRepository : IRepository<ServoTemp>
    {
        Task<IEnumerable<ServoTemp>> GetAll(Guid grupoOracaoId);
        Task<IEnumerable<ServoTemp>> GetAll();
        Task<bool> Disable(Guid id);

        bool ValidateServoTemp(string name, string birthday, string cpf, string email, string cellPhone);
        Task<ServoTemp> GetById(Guid id);
        Task<IEnumerable<ServoTemp>> GetByNameCpfEmail(string name, string cpf, string email);
        Task<IEnumerable<ServoTemp>> GetByCpfGrupoOracao(Guid grupoOracaoId, string cpf);

    }
}

