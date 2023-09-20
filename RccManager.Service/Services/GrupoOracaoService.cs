using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;

namespace RccManager.Service.Services
{
	public class GrupoOracaoService : IGrupoOracaoService
	{
        private readonly IMapper mapper;
        private readonly IGrupoOracaoRepository repository;

        public GrupoOracaoService(IMapper mapper, IGrupoOracaoRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<HttpResponse> Create(GrupoOracaoDto grupoOracao)
        {

            grupoOracao.FoundationDate = formatFoundationDate(grupoOracao.FoundationDate1);
            grupoOracao.Time = formaTime(grupoOracao.Time1);

            var grupoOracao_ = mapper.Map<GrupoOracao>(grupoOracao);
            var result = await repository.Insert(grupoOracao_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar Grupo de Oração", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Grupo de Oração criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<GrupoOracaoDtoResult>> GetAll(string search)
        {
            var entities = await repository.GetAll(search);
            return mapper.Map<IEnumerable<GrupoOracaoDtoResult>>(entities);
        }

        public async Task<HttpResponse> Update(GrupoOracaoDto grupoOracao, Guid id)
        {
            var grupoOracao_ = mapper.Map<GrupoOracao>(grupoOracao);
            grupoOracao_.Id = id;
            grupoOracao_.FoundationDate = formatFoundationDate(grupoOracao.FoundationDate1);
            grupoOracao_.Time = formaTime(grupoOracao.Time1);

            var result = await repository.Update(grupoOracao_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Grupo de Oração atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        private DateTime formatFoundationDate(string date)
        {
            var year = int.Parse(date.Substring(4));
            var day = int.Parse(date.Substring(0, 2));
            var month = int.Parse(date.Substring(2, 2));

            return new DateTime(year, month, day);
        }

        private DateTime formaTime(string time)
        {
            var hours = int.Parse(time.Substring(0, 2));
            var minutes = int.Parse(time.Substring(2));

            var now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, hours, minutes, 0);
        }
    }
}

