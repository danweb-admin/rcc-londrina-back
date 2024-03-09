using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Enum;
using RccManager.Service.Helper;

namespace RccManager.Service.Services
{
	public class GrupoOracaoService : IGrupoOracaoService
	{
        private readonly IMapper mapper;
        private readonly IGrupoOracaoRepository repository;
        private readonly IHistoryRepository history;

        public GrupoOracaoService(IMapper _mapper, IGrupoOracaoRepository _repository, IHistoryRepository _history)
        {
            mapper = _mapper;
            repository = _repository;
            history = _history;
        }

        public async Task<HttpResponse> Create(GrupoOracaoDto grupoOracao)
        {

            grupoOracao.FoundationDate = Utils.formatDate(grupoOracao.FoundationDate1);
            grupoOracao.Time = Utils.formaTime(grupoOracao.Time1);

            var grupoOracao_ = mapper.Map<GrupoOracao>(grupoOracao);
            var result = await repository.Insert(grupoOracao_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar Grupo de Oração", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            await history.Add(TableEnum.GrupoOracao.ToString(), result.Id, OperationEnum.Criacao.ToString());

            return new HttpResponse { Message = "Grupo de Oração criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<GrupoOracaoDtoResult>> GetAll(string search, UserDtoResult user)
        {
            var user_ = mapper.Map<User>(user);
            var entities = await repository.GetAll(search, user_);
            return mapper.Map<IEnumerable<GrupoOracaoDtoResult>>(entities);
        }

        public async Task<HttpResponse> Update(GrupoOracaoDto grupoOracao, Guid id)
        {
            var grupoOracao_ = mapper.Map<GrupoOracao>(grupoOracao);
            grupoOracao_.Id = id;
            grupoOracao_.FoundationDate = Utils.formatDate(grupoOracao.FoundationDate1);
            grupoOracao_.Time = Utils.formaTime(grupoOracao.Time1);

            var result = await repository.Update(grupoOracao_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            await history.Add(TableEnum.GrupoOracao.ToString(), result.Id, OperationEnum.Alteracao.ToString());

            return new HttpResponse { Message = "Grupo de Oração atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        

        
    }
}

