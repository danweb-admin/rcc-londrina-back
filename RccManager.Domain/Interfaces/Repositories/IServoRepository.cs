using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories;

public interface IServoRepository : IRepository<Servo>
{
    Task<IEnumerable<Servo>> GetAll(Guid grupoOracaoId);
    Task<bool> GetByCPF(string cpf);
    Task<bool> GetByCPF(Guid id, string cpf);
    Task<bool> GetByEmail(string email);
    Task<bool> GetByEmail(Guid id, string email);

}

