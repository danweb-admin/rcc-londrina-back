using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories;

public interface IServoRepository : IRepository<Servo>
{
    new Task<Servo> GetById(Guid id);
    Task<IEnumerable<Servo>> GetAll(Guid grupoOracaoId);
    Task<IEnumerable<Servo>> GetAll();
    Task<bool> GetByCPF(string cpf);
    Task<bool> GetByCPF(Guid id, string cpf);
    Task<Servo> GetServoByCPF(string cpf);
    Task<bool> GetByEmail(string email);
    Task<bool> GetByEmail(Guid id, string email);

}

