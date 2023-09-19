﻿using System;
using System.ComponentModel.DataAnnotations;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
	public interface IUserService
	{
        Task<IEnumerable<UserDtoResult>> GetAll();

        Task<UserDtoResult> GetById(Guid Id);

        Task<UserDtoResult> GetByName(string Name);

        Task<HttpResponse> Add(UserDtoAdd user);

        Task<HttpResponse> Update(UserDto user, Guid id);

        Task<HttpResponse> ChangeUserPassword(UserDtoResult user, string newPassword);

        Task<UserDto> Authenticate(string email, string password);

        Task<UserDtoResult> GetByEmail(string email);
    }
}

