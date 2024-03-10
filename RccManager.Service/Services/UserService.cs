using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Domain.Helpers;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Enum;

namespace RccManager.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IMD5Service mD5Service;
    private readonly IHistoryRepository history;

    public UserService(IUserRepository _userRepository, IMapper _mapper, IMD5Service _mD5Service, IHistoryRepository _history)
    {
        userRepository = _userRepository;
        mapper = _mapper;
        mD5Service = _mD5Service;
        history = _history;
    }

    public async Task<HttpResponse> Add(UserDtoAdd user)
    {
        var _user = mapper.Map<User>(user);

        var isValid = await userRepository.GetByEmail(_user.Email);

        if (isValid != null)
            return new HttpResponse { Message = "Já existe um usuario com este email.", StatusCode = (int)HttpStatusCode.NotFound };

        _user.Password = mD5Service.ReturnMD5(_user.Password);

        var result = await userRepository.Insert(_user);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para criar o Usuário", StatusCode = (int)HttpStatusCode.BadRequest };

        // adiciona a tabela de histórico de alteracao
        await history.Add(TableEnum.User.ToString(), result.Id, OperationEnum.Criacao.ToString());

        return new HttpResponse { Message = "Usuário criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };


    }

    public async Task<UserDto> Authenticate(string email, string password)
    {
        var user = await userRepository.GetByEmail(email);

        if (user != null)
        {
            if (!user.Active)
                throw new Exception("Usuário não esta ativo.");

            if (mD5Service.CompareMD5(password, user.Password))
            {
                // adiciona a tabela de histórico de alteracao
                await history.Add(TableEnum.User.ToString(), user.Id, OperationEnum.Logou.ToString());

                return mapper.Map<UserDto>(user);
            }
        }

        return null;
    }

    public async Task<IEnumerable<UserDtoResult>> GetAll(string search)
    {
        return mapper.Map<IEnumerable<UserDtoResult>>(await userRepository.GetAll(search));
    }

    public async Task<UserDtoResult> GetByEmail(string email)
    {
        return mapper.Map<UserDtoResult>(await userRepository.GetByEmail(email));
    }

    public async Task<UserDtoResult> GetById(Guid Id)
    {
        return mapper.Map<UserDtoResult>(await userRepository.GetById(Id));
    }

    public async Task<UserDtoResult> GetByName(string name)
    {
        return mapper.Map<UserDtoResult>(await userRepository.GetByName(name));

    }

    public async Task<HttpResponse> Update(UserDto user, Guid id)
    {
        var temp = await userRepository.GetById(id);
        var _user = mapper.Map<User>(user);

        _user.Id = id;
        _user.Password = temp.Password;

        var result = await userRepository.Update(_user);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para atualizar o Usuário", StatusCode = (int)HttpStatusCode.BadRequest };

        // adiciona a tabela de histórico de alteracao
        await history.Add(TableEnum.User.ToString(), result.Id, OperationEnum.Alteracao.ToString());

        return new HttpResponse { Message = "Usuário atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<HttpResponse> ChangeUserPassword(UserDtoResult user, string newPassword)
    {
        var _user = mapper.Map<User>(user);
        _user.UpdatedAt = Helpers.DateTimeNow();
        _user.Password = mD5Service.ReturnMD5(newPassword);

        var result = await userRepository.Update(_user);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para mudar a senha.", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Senha atualizada com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<UserDtoResult> GetUserContext(ClaimsPrincipal claimsPrincipal)
    {
        var email = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

        if (email != null)
        {
            var email_ = await userRepository.GetByEmail(email.Value);
            return mapper.Map<UserDtoResult>(email_);
        }

        return null;
    }
}

