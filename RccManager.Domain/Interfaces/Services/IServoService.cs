using System;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services;

public interface IServoService
{
    Task<IEnumerable<ServoDtoResult>> GetAll(Guid grupoOracaoId);

    Task<HttpResponse> Create(ServoDto servo);

    Task<HttpResponse> Update(ServoDto servo, Guid id);
}

