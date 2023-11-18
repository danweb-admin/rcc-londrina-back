using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IFormacoesServoRepository : IRepository<FormacoesServo>
    {
        Task<IEnumerable<FormacoesServo>> GetAll(Guid servoId);
        Task<bool> GetByServoAndFormacao(Guid formacaoId, Guid servoId);

    }
}

