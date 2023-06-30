using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;

namespace RccManager.Service.Services
{
    public class DecanatoSetorService : IDecanatoSetorService
    {
        private readonly IMapper mapper;
        private IDecanatoSetorRepository repository;

        public DecanatoSetorService(IMapper _mapper, IDecanatoSetorRepository _repository)
        {
            this.mapper = _mapper;
            this.repository = _repository;
        }

        public async Task<HttpResponse> Create(DecanatoSetorDto decanatoSetorViewModel)
        {
            
            if (await repository.GetByName(decanatoSetorViewModel.Name))
                throw new ValidateByNameException("ERRO: Esse decanato/setor já existe.");

            var decanato = mapper.Map<DecanatoSetor>(decanatoSetorViewModel);



            var result = await repository.Insert(decanato);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Objeto criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<HttpResponse> Delete(Guid id)
        {
            var result = await repository.Delete(id);

            if (result)
                return new HttpResponse { Message = "Objeto removido com sucesso.", StatusCode = (int)HttpStatusCode.OK };

            return new HttpResponse { Message = "Houve um problema para remover o objeto", StatusCode = (int)HttpStatusCode.BadRequest };
        }

        public async Task<HttpResponse> ActivateDeactivate(Guid id)
        {
            var entity = await repository.GetById(id);
            entity.Active = !entity.Active;

            var result = await repository.Update(entity);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para ativar/inativar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Objeto ativado/inativado com sucesso.", StatusCode = (int)HttpStatusCode.OK };

        }

        public async Task<IEnumerable<DecanatoSetorDtoResult>> GetAll()
        {
            return mapper.Map<IEnumerable<DecanatoSetorDtoResult>>(await repository.GetAll());
        }

        public async Task<HttpResponse> Update(DecanatoSetorDto decanatoSetorViewModel, Guid id)
        {
            if (await repository.GetByName(decanatoSetorViewModel.Name, id))
                throw new ValidateByNameException("ERRO: Esse decanato/setor já existe.");

            var decanato = mapper.Map<DecanatoSetor>(decanatoSetorViewModel);
            decanato.Id = id;

            var result = await repository.Update(decanato);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Objeto atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

    }
}

