using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
    public interface IGrupoOracaoService
	{
        Task<IEnumerable<GrupoOracaoDtoResult>> GetAll(string search, UserDtoResult user);

        Task<HttpResponse> Create(GrupoOracaoDto grupoOracao);

        Task<HttpResponse> Update(GrupoOracaoDto grupoOracao, Guid id);

        Task<HttpResponse> ImportCSV(Guid id, UserDtoResult user);

    }
}

