using System;
using Microsoft.IdentityModel.Tokens;
using RccManager.Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RccManager.Domain.Dtos.Users;

namespace RccManager.Test.Services
{
    public class TokenServiceTests
    {

        [Fact]
        public void GenerateToken_WithValidUser_ReturnsToken()
        {
            // Arrange
            var tokenService = new TokenService();
            Environment.SetEnvironmentVariable("KeyMD5", "mNjTWFuQGczckAyMDI");
            var keyMd5 = Environment.GetEnvironmentVariable("KeyMD5");
            var user = new UserDto { Name = "John Doe", Email = "email@email.com"};

            // Act
            var token = tokenService.GenerateToken(user);

            // Assert
            Assert.NotNull(token);

            // Parse the token to ensure it's a valid token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(keyMd5);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;

            Assert.NotNull(claimsPrincipal);
            Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
            Assert.Equal("John Doe", claimsPrincipal.FindFirstValue(ClaimTypes.Name));
        }

        [Fact]
        public void GenerateToken_WithNullUser_ReturnsNull()
        {
            // Arrange
            var tokenService = new TokenService();
            UserDto user = null;

            // Act
            var token = tokenService.GenerateToken(user);

            // Assert
            Assert.Null(token);
        }
    }
}

