using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.InscricoesEvento;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Servo;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Helper;

namespace RccManager.Service.Services
{
	public class InscricoesEventoService : IInscricoesEventoService
	{
        private readonly IMapper _mapper;
        private readonly IInscricoesEventoRepository _repository;
        private readonly IServoRepository _repositoryServo;
        private readonly IEventoRepository _repositoryEvento;


        public InscricoesEventoService(IMapper mapper, IInscricoesEventoRepository repository, IServoRepository repositoryServo, IEventoRepository repositoryEvento)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryServo = repositoryServo;
            _repositoryEvento = repositoryEvento;
        }

        public async Task<HttpResponse> Create(InscricoesEventoDto inscricao)
        {
            inscricao.Birthday = Utils.formatDate2(inscricao.Birthday1);
            var inscricao_ = _mapper.Map<PagamentoAsaas>(inscricao);

            var result = await _repository.Insert(inscricao_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar a Inscrição", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Inscrição criada com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<HttpResponse> CreateInscricaoByCpf(string cpf, Guid eventId)
        {
            var exists = await _repositoryServo.GetServoByCPF(Utils.Encrypt(cpf));

            var format = "yyyy-MM-dd";

            if (exists == null)
                return new HttpResponse { Message = "Não foi encontrado cadastro para o CPF informado.", StatusCode = (int)HttpStatusCode.BadRequest };

            var check = await _repository.CheckByCpf(eventId, cpf);

            if (check)
                return new HttpResponse { Message = "Este CPF já foi inscrito nesse Evento.", StatusCode = (int)HttpStatusCode.BadRequest };

            var evento = await _repositoryEvento.GetById(eventId);

            var inscricao = new InscricoesEventoDto
            {
                Birthday = exists.Birthday,
                Birthday1 = exists.Birthday.ToString(format),
                CellPhone = Utils.Decrypt(exists.CellPhone),
                Cpf = Utils.Decrypt(exists.Cpf),
                Email = Utils.Decrypt(exists.Email),
                Name = Utils.Decrypt(exists.Name),
                GrupoOracaoId = exists.GrupoOracaoId,
                Registered = "S",
                Status = "pending",
                AmountPaid = 0,
                
                EventId = eventId
            };

            var result =  await Create(inscricao);

            return result;


        }

        public Task<IEnumerable<InscricoesEventoDtoResult>> GetAll(Guid eventoId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponse> Update(InscricoesEventoDto inscricao, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

