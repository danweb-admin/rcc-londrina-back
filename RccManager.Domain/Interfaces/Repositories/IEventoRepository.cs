using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
    public interface IEventoRepository : IRepository<Evento>
    {
        Task<IEnumerable<Evento>> GetAll(bool active);
        Task<IEnumerable<Evento>> GetAll();
        Task<Evento> GetById(Guid id);

    }
}
