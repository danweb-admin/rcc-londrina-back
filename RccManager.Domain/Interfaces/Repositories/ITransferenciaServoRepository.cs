using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
    public interface ITransferenciaServoRepository : IRepository<TransferenciaServo>
    {
            Task<IEnumerable<TransferenciaServo>> GetAll();

    }
}

