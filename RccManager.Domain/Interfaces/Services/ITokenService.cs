using System;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Dtos.UsuarioCheckin;

namespace RccManager.Domain.Interfaces.Services
{
	public interface ITokenService
	{
        string GenerateToken(UserDto user);
        string GenerateTokenCheckin(UsuarioCheckinDtoResult user);
    }
}

