using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.EventoUsuarios;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Enum;

namespace RccManager.Service.Services
{
    public class EventoUsuariosService : IEventoUsuariosService
    {
        private readonly IMapper mapper;
        private IEventoUsuariosRepository repository;
        private IHistoryRepository history;


        public EventoUsuariosService(IMapper _mapper, IEventoUsuariosRepository _repository, IHistoryRepository _history)
        {
            mapper = _mapper;
            repository = _repository;
            history = _history;
        }

        public Task<HttpResponse> ActivateDeactivate(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponse> Create(EventoUsuariosDto eventoUsuariosDto)
        {
            

            var eventoUsuario = mapper.Map<EventoUsuarios>(eventoUsuariosDto);

            var result = await repository.Insert(eventoUsuario);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            await history.Add(TableEnum.DecanatoSetor.ToString(), result.Id, OperationEnum.Criacao.ToString());

            return new HttpResponse { Message = "Objeto criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public Task<HttpResponse> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventoUsuariosDtoResult>> GetAll(bool active)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventoUsuariosDtoResult>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponse> Update(EventoUsuariosDto eventoUsuariosDto, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

