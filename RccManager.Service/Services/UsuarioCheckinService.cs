using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.UsuarioCheckin;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Enum;
using static StackExchange.Redis.Role;

namespace RccManager.Service.Services
{
    public class UsuarioCheckinService : IUsuarioCheckinService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioCheckinRepository _repository;

        public UsuarioCheckinService(IMapper mapper, IUsuarioCheckinRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<HttpResponse> Create(UsuarioCheckinDto usuarioCheckin)
        {
            var objeto = _mapper.Map<UsuariosCheckin>(usuarioCheckin);


            var result = await _repository.Insert(objeto);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Objeto criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<UsuarioCheckinDtoResult>> GetAll(string email)
        {
            return _mapper.Map<IEnumerable<UsuarioCheckinDtoResult>>(await _repository.GetAll(email));
            

        }

        public Task<IEnumerable<UsuarioCheckinDtoResult>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponse> Login(string email, string senha)
        {
            var result = await _repository.Login(email, senha);
            if (!result)
                return new HttpResponse { Message = "Email e senha estão incorretos", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Login realizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<HttpResponse> Update(UsuarioCheckinDto usuarioCheckin, Guid id)
        {
            

            var objeto = _mapper.Map<UsuariosCheckin>(usuarioCheckin);
            objeto.Id = id;

            var result = await _repository.Update(objeto);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Objeto atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }
    }
}

