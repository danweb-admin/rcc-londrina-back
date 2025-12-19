using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
    public interface IEventoRepository : IRepository<Evento>
    {
        Task<IEnumerable<Evento>> GetAll(string status);
        Task<IEnumerable<Evento>> GetAllHome();
        Task<IEnumerable<Evento>> GetAll();
        Task<Evento> GetById(Guid id);
        Task<string> GetSlug(Guid id);
        Task<Evento> Update(Evento evento);

    }
}
