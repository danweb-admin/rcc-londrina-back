using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
    public interface IGrupoOracaoRepository : IRepository<GrupoOracao>
    {
        Task<IEnumerable<GrupoOracao>> GetAll(string search, User user);
        Task<GrupoOracao> GetByName(string name, string paroquiaCapelaName);
    }
}

