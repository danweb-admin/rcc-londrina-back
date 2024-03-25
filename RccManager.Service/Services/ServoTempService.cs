using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using System.Net;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Helper;
using RccManager.Domain.Responses;
using AutoMapper;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Exception.Servo;
using RccManager.Domain.Helpers;
using RccManager.Service.Enum;

namespace RccManager.Service.Services
{
    public class ServoTempService : IServoTempService
    {
        private readonly IMapper _mapper;
        private readonly IServoTempRepository _repository;
        private readonly IServoRepository _repo;
        private readonly IGrupoOracaoRepository _repositoryGO;
        private readonly IHistoryRepository _history;


        public ServoTempService(IMapper mapper, IServoTempRepository repository, IGrupoOracaoRepository repositoryGO, IServoRepository repo, IHistoryRepository history)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryGO = repositoryGO;
            _repo = repo;
            _history = history;
        }

        public async Task<HttpResponse> Checked(Guid id)
        {
            var servoTemp = await _repository.GetById(id);

            var exists = await _repo.GetByCPF(servoTemp.Cpf);

            if (exists)
                throw new ValidateByCpfOrEmailException("Este CPF já está sendo utilizado.");

            exists = await _repo.GetByEmail(servoTemp.Email);

            if (exists)
                throw new ValidateByCpfOrEmailException("Este EMAIL já está sendo utilizado.");

            var servo = new Servo
            {
                Active = servoTemp.Active,
                CellPhone = servoTemp.CellPhone,
                Birthday = DateTime.Parse(servoTemp.Birthday),
                Cpf = servoTemp.Cpf,
                CreatedAt = Helpers.DateTimeNow(),
                Email = servoTemp.Email,
                GrupoOracaoId = servoTemp.GrupoOracaoId,
                MainMinistry = servoTemp.MainMinistry,
                SecondaryMinistry = servoTemp.SecondaryMinistry,
                Name = servoTemp.Name,

            };

            await _repo.Insert(servo);

            // adiciona a tabela de histórico de alteracao
            await _history.Add(TableEnum.Servo.ToString(), servo.Id, OperationEnum.Criacao.ToString());

            servoTemp.Checked = true;

            var result = await _repository.Update(servoTemp);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para validar Servo(a)", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            await _history.Add(TableEnum.ServoTemp.ToString(), result.Id, OperationEnum.Validado.ToString());

            return new HttpResponse { Message = "Servo(a) validado com sucesso.", StatusCode = (int)HttpStatusCode.OK };

        }

        public HttpResponse Create(ServoTempDto servo)
        {
            var exists = _repository.ValidateServoTemp(Utils.Encrypt(servo.Name), DateTime.Parse(servo.Birthday), Utils.Encrypt(servo.Cpf), Utils.Encrypt(servo.Email), Utils.Encrypt(servo.CellPhone));

            if (exists)
                return new HttpResponse { Message = "Servo(a) temporário já existe", StatusCode = (int)HttpStatusCode.BadRequest };

            servo.Name = servo.Name.ToUpper();
            servo.MainMinistry = Ministerios.returnMinistryValue(servo.MainMinistry);
            if (!string.IsNullOrEmpty(servo.SecondaryMinistry))
                servo.SecondaryMinistry = Ministerios.returnMinistryValue(servo.SecondaryMinistry);

            var servo_ = _mapper.Map<ServoTemp>(servo);

            var grupoOracao = _repositoryGO.GetByName(servo.GrupoOracaoName, servo.ParoquiaCapelaName);
            servo_.GrupoOracaoId = grupoOracao.Id;

            var result =  _repository.Insert(servo_).GetAwaiter().GetResult();

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar Servo(a)", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            _history.Add(TableEnum.ServoTemp.ToString(), result.Id, OperationEnum.Criacao.ToString());

            return new HttpResponse { Message = "Servo(a) criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<ServoTempDtoResult>> GetAll(Guid grupoOracaoId)
        {
            var list = _mapper.Map<IEnumerable<ServoTempDtoResult>>(await _repository.GetAll(grupoOracaoId));
            return list.OrderBy(x => x.Name);
        }

        public async Task<HttpResponse> Update(ServoTempDto servoTemp, Guid id)
        {
            servoTemp.Birthday = servoTemp.Birthday;
            servoTemp.Cpf = servoTemp.Cpf.Replace(" ", "").Replace(".", "").Replace("-", "");
            servoTemp.CellPhone = servoTemp.CellPhone.Replace(" ", "").Replace(".", "").Replace("-", "").Replace("(","").Replace(")","");

            var exists = await _repo.GetByCPF(Utils.Encrypt(servoTemp.Cpf));

            if (exists)
                throw new ValidateByCpfOrEmailException("Este CPF já está sendo utilizado.");

            var servo_ = _mapper.Map<ServoTemp>(servoTemp);
            servo_.Id = id;
            servo_.Checked = true;

            var servo = new Servo
            {
                Active = servo_.Active,
                CellPhone = servo_.CellPhone,
                Birthday = Utils.formatDate2(servo_.Birthday),
                Cpf = servo_.Cpf,
                CreatedAt = Helpers.DateTimeNow(),
                Email = servo_.Email,
                GrupoOracaoId = servo_.GrupoOracaoId,
                MainMinistry = servo_.MainMinistry,
                SecondaryMinistry = servo_.SecondaryMinistry,
                Name = servo_.Name,

            };

            var result = await _repository.Update(servo_);

            await _repo.Insert(servo);

            // adiciona a tabela de histórico de alteracao
            await _history.Add(TableEnum.Servo.ToString(), servo.Id, OperationEnum.Criacao.ToString());

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para validar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            await _history.Add(TableEnum.ServoTemp.ToString(), result.Id, OperationEnum.Alteracao.ToString());

            return new HttpResponse { Message = "Servo(a) validado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public Task<HttpResponse> UploadFile(StreamReader reader, Guid grupoOracaoId)
        {
            throw new NotImplementedException();
        }
    }
}

