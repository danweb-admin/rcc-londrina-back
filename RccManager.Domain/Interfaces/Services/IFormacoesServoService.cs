using RccManager.Domain.Dtos.FormacoesServo;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services;

public interface IFormacoesServoService 
{
    Task<IEnumerable<FormacoesServoDtoResult>> GetAll(Guid servoId);

    Task<HttpResponse> Create(FormacoesServoDtoCreate viewModel);

    Task<HttpResponse> Delete(Guid id);
}

