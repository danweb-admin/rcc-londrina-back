using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Dtos.TransferenciaServico;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Servo;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Helper;

namespace RccManager.Service.Services
{
    public class TransferenciaServoService : ITransferenciaServoService
    {
        private readonly IMapper _mapper;
        private readonly ITransferenciaServoRepository _repository;
        private readonly IServoRepository _repositoryServo;


        public TransferenciaServoService(IMapper mapper, ITransferenciaServoRepository repository, IServoRepository repositoryServo)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryServo = repositoryServo;
        }

        public async Task<HttpResponse> Create(TransferenciaServoDto transferencia, UserDtoResult user)
        {
            if (transferencia.GrupoOracaoId == transferencia.GrupoOracaoAntigoId)
                throw new WebException("Alteração não permitida, servo não pode ser transferido para o mesmo Grupo de Oração.");

            transferencia.Efetuado = user.Name;

            var servo_ = _mapper.Map<TransferenciaServo>(transferencia);

            var result = await _repository.Insert(servo_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar Transferencia de Servo(a)", StatusCode = (int)HttpStatusCode.BadRequest };

            var servo = await _repositoryServo.GetById(transferencia.ServoId);

            servo.GrupoOracaoId = transferencia.GrupoOracaoId;
            servo.UpdatedAt = DateTime.Now;

             await _repositoryServo.Update(servo);


            return new HttpResponse { Message = "Transferencia de Servo(a) criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<TransferenciaServoDtoResult>> GetAll()
        {
            return _mapper.Map<IEnumerable<TransferenciaServoDtoResult>>(await _repository.GetAll());
        }
    }
}

