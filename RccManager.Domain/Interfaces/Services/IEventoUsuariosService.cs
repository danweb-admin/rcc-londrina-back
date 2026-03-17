using System;
using RccManager.Domain.Dtos.EventoUsuarios;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
    public interface IEventoUsuariosService
    {
        Task<IEnumerable<EventoUsuariosDtoResult>> GetAll(bool active);
        Task<IEnumerable<EventoUsuariosDtoResult>> GetAll();
        Task<HttpResponse> Create(EventoUsuariosDto eventoUsuariosDto);

        Task<HttpResponse> Update(EventoUsuariosDto eventoUsuariosDto, Guid id);

        Task<HttpResponse> Delete(Guid id);

        Task<HttpResponse> ActivateDeactivate(Guid id);
    }
}

