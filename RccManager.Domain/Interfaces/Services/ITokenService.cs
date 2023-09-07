using System;
using RccManager.Domain.Dtos.Users;

namespace RccManager.Domain.Interfaces.Services
{
	public interface ITokenService
	{
        string GenerateToken(UserDto user);
    }
}

