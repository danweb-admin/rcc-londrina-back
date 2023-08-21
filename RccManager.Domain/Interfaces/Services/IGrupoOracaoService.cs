using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
    public interface IGrupoOracaoService
	{
        Task<IEnumerable<GrupoOracaoDtoResult>> GetAll();

        Task<GrupoOracaoDtoResult> GetById(Guid Id);

        Task<HttpResponse> Add(GrupoOracaoDto grupoOracao);

        Task<HttpResponse> Update(GrupoOracaoDtoResult grupoOracao, Guid id);
    }
}

