using System;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Dtos.TransferenciaServico;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
    public interface ITransferenciaServoService
    {
        Task<IEnumerable<TransferenciaServoDtoResult>> GetAll();
        Task<HttpResponse> Create(TransferenciaServoDto servo, UserDtoResult user);

    }
}

