using System.Net;
using AutoMapper;
using Moq;
using RccManager.Application.Mapper;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.Tests.Service.Services
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMD5Service> _md5ServiceMock;
        private readonly IUserService _service;

        public UserServiceTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new DtoToEntityProfile());
            }).CreateMapper();

            _userRepositoryMock = new Mock<IUserRepository>();
            _md5ServiceMock = new Mock<IMD5Service>();
            _service = new UserService(_userRepositoryMock.Object, _mapper, _md5ServiceMock.Object);
        }

        [Fact]
        public async Task Add_WithValidUserDto_ReturnsSuccessResponse()
        {
            // Arrange
            var userDto = new UserDtoAdd { Email = "test@example.com", Password = "password123" };
            var userEntity = _mapper.Map<User>(userDto);

            _userRepositoryMock.Setup(repo => repo.GetByEmail(userDto.Email)).ReturnsAsync((User)null);
            _md5ServiceMock.Setup(service => service.ReturnMD5(userDto.Password)).Returns("hashedPassword");
            _userRepositoryMock.Setup(repo => repo.Insert(It.IsAny<User>())).ReturnsAsync(userEntity);

            // Act
            var result = await _service.Add(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Usuário criado com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Add_WithExistingEmail_ReturnsErrorResponse()
        {
            // Arrange
            var existingEmail = "existing@example.com";
            var userDto = new UserDtoAdd { Email = existingEmail, Password = "password123" };

            _userRepositoryMock.Setup(repo => repo.GetByEmail(existingEmail)).ReturnsAsync(new User());

            // Act
            var result = await _service.Add(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Já existe um usuario com este email.", result.Message);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Authenticate_WithValidCredentials_ReturnsUserDto()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password123";
            var hashedPassword = "hashedPassword";
            var userEntity = new User { Email = email, Password = hashedPassword, Active = true };
            var userDto = _mapper.Map<UserDto>(userEntity);

            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(userEntity);
            _md5ServiceMock.Setup(service => service.CompareMD5(password, hashedPassword)).Returns(true);

            // Act
            var result = await _service.Authenticate(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDto>(result);
        }

        [Fact]
        public async Task Authenticate_WithInactiveUser_ReturnsNull()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password123";
            var userEntity = new User { Email = email, Password = "hashedPassword", Active = true };

            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(userEntity);

            // Act
            var result = await _service.Authenticate(email, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Authenticate_WithInvalidCredentials_ReturnsNull()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password123";
            var hashedPassword = "hashedPassword";
            var userEntity = new User { Email = email, Password = hashedPassword, Active = true };

            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(userEntity);
            _md5ServiceMock.Setup(service => service.CompareMD5(password, hashedPassword)).Returns(false);

            // Act
            var result = await _service.Authenticate(email, password);

            // Assert
            Assert.Null(result);
        }
    }
}
