using System.Net;
using AutoMapper;
using Moq;
using RccManager.Application.Mapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.Tests.Service.Services
{
    public class DecanatoSetorServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDecanatoSetorRepository> _repositoryMock;
        private readonly IDecanatoSetorService _service;

        public DecanatoSetorServiceTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new DtoToEntityProfile());
            }).CreateMapper();

            _repositoryMock = new Mock<IDecanatoSetorRepository>();
            _service = new DecanatoSetorService(_mapper, _repositoryMock.Object);
        }

        [Fact]
        public async Task Create_WithValidDto_ReturnsSuccessResponse()
        {
            // Arrange
            var dto = new DecanatoSetorDto { Name = "Test Decanato" };
            var entity = _mapper.Map<DecanatoSetor>(dto);

            _repositoryMock.Setup(repo => repo.GetByName(dto.Name)).ReturnsAsync(false);
            _repositoryMock.Setup(repo => repo.Insert(It.IsAny<DecanatoSetor>())).ReturnsAsync(entity);

            // Act
            var result = await _service.Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Objeto criado com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Create_WithExistingDto_ThrowsValidateByNameException()
        {
            // Arrange
            var dto = new DecanatoSetorDto { Name = "Existing Decanato" };

            _repositoryMock.Setup(repo => repo.GetByName(dto.Name)).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<ValidateByNameException>(() => _service.Create(dto));
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsSuccessResponse()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.Delete(id)).ReturnsAsync(true);

            // Act
            var result = await _service.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Objeto removido com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsErrorResponse()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.Delete(id)).ReturnsAsync(false);

            // Act
            var result = await _service.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Houve um problema para remover o objeto", result.Message);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ActivateDeactivate_WithValidId_ReturnsSuccessResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new DecanatoSetor { Id = id, Active = true };

            _repositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(entity);
            _repositoryMock.Setup(repo => repo.Update(It.IsAny<DecanatoSetor>())).ReturnsAsync(entity);

            // Act
            var result = await _service.ActivateDeactivate(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Objeto ativado/inativado com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.False(entity.Active);
        }

        [Fact]
        public async Task ActivateDeactivate_WithInvalidId_ReturnsErrorResponse()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(new DecanatoSetor { Name = "Teste", Id = id});

            // Act
            var result = await _service.ActivateDeactivate(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Houve um problema para ativar/inativar o objeto", result.Message);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfDtoResults()
        {
            // Arrange
            var entities = new List<DecanatoSetor>
            {
                new DecanatoSetor { Id = Guid.NewGuid(), Name = "Decanato 1" },
                new DecanatoSetor { Id = Guid.NewGuid(), Name = "Decanato 2" },
                new DecanatoSetor { Id = Guid.NewGuid(), Name = "Decanato 3" }
            };

            _repositoryMock.Setup(repo => repo.GetAll(string.Empty)).ReturnsAsync(entities);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<DecanatoSetorDtoResult>>(result);
            Assert.Equal(entities.Count, result?.Count());
        }

        [Fact]
        public async Task Update_WithValidDtoAndId_ReturnsSuccessResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new DecanatoSetorDto { Name = "Updated Decanato" };
            var entity = _mapper.Map<DecanatoSetor>(dto);

            _repositoryMock.Setup(repo => repo.GetByName(dto.Name, id)).ReturnsAsync(false);
            _repositoryMock.Setup(repo => repo.Update(It.IsAny<DecanatoSetor>())).ReturnsAsync(entity);

            // Act
            var result = await _service.Update(dto, id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Objeto atualizado com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Update_WithExistingDto_ThrowsValidateByNameException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new DecanatoSetorDto { Name = "Existing Decanato" };

            _repositoryMock.Setup(repo => repo.GetByName(dto.Name, id)).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<ValidateByNameException>(() => _service.Update(dto, id));
        }
    }
}
