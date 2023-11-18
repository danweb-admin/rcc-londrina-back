using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services;

public interface IFormacaoService
{
    Task<IEnumerable<FormacaoDtoResult>> GetAll(bool active);
    Task<IEnumerable<FormacaoDtoResult>> GetAll();
    Task<HttpResponse> Create(FormacaoDto formacaoViewModel);

    Task<HttpResponse> Update(FormacaoDto formacaoViewModel, Guid id);

    Task<HttpResponse> Delete(Guid id);

    Task<HttpResponse> ActivateDeactivate(Guid id);
}

