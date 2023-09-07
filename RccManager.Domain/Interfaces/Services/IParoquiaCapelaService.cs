using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
    public interface IParoquiaCapelaService
	{
        Task<IEnumerable<ParoquiaCapelaDtoResult>> GetAll(string search);

        Task<HttpResponse> Create(ParoquiaCapelaDto model);

        Task<HttpResponse> Update(ParoquiaCapelaDto model, Guid id);

        Task<HttpResponse> Delete(Guid id);

        Task<HttpResponse> ActivateDeactivate(Guid id);
    }
}

