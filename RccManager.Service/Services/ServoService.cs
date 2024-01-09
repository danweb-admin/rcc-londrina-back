using System;
using System.Net;
using AutoMapper;
using Newtonsoft.Json;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Exception.Servo;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Helper;
using static StackExchange.Redis.Role;

namespace RccManager.Service.Services
{
	public class ServoService : IServoService
	{
        private readonly IMapper _mapper;
        private readonly IServoRepository _repository;


        public ServoService(IMapper mapper, IServoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<HttpResponse> Create(ServoDto servo)
        {
            servo.Birthday = Utils.formatDate(servo.Birthday1);
            var servo_ = _mapper.Map<Servo>(servo);

            var exists = await _repository.GetByCPF(Utils.Encrypt(servo.Cpf));

            if (exists)
                throw new ValidateByCpfOrEmailException("Este CPF já está sendo utilizado.");

            exists = await _repository.GetByEmail(Utils.Encrypt(servo.Email));

            if (exists)
                throw new ValidateByCpfOrEmailException("Este EMAIL já está sendo utilizado.");

            var result = await _repository.Insert(servo_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar Servo(a)", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Servo(a) criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<ServoDtoResult>> GetAll(Guid grupoOracaoId)
        {
            var list = _mapper.Map<IEnumerable<ServoDtoResult>>(await _repository.GetAll(grupoOracaoId));
            return list.OrderBy(x => x.Name);

        }

        public async Task<HttpResponse> Update(ServoDto servo, Guid id)
        {
            servo.Birthday = Utils.formatDate(servo.Birthday1);
            var exists = await _repository.GetByCPF(id ,Utils.Encrypt(servo.Cpf));

            if (exists)
                throw new ValidateByCpfOrEmailException("Este CPF já está sendo utilizado.");

            exists = await _repository.GetByEmail(id, Utils.Encrypt(servo.Email));

            if (exists)
                throw new ValidateByCpfOrEmailException("Este EMAIL já está sendo utilizado.");

            var servo_ = _mapper.Map<Servo>(servo);
            servo_.Id = id;

            var result = await _repository.Update(servo_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Servo(a) atualizado(a) com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }
    }
}

