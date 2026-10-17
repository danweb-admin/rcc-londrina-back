using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
    public interface IFormularioCadastroServoRepository : IRepository<FormularioCadastroServo>
    {
        Task<FormularioCadastroServo> GetByGrupoOracaoId(Guid grupoOracaoId);
        Task<FormularioCadastroServo> GetByToken(string token);

    }
}

