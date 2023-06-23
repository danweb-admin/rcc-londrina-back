using System;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services;

public interface IDecanatoSetorService
{
    Task<IEnumerable<DecanatoSetorDtoResult>> GetAll();

    Task<HttpResponse> Create(DecanatoSetorDto decanatoSetorViewModel);

    Task<HttpResponse> Update(DecanatoSetorDto decanatoSetorViewModel, Guid id);

    Task<HttpResponse> Delete(Guid id);

    Task<HttpResponse> ActivateDeactivate(Guid id);

}

