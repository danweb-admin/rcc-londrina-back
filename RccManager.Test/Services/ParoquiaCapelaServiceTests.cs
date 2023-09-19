using System.Net;
using AutoMapper;
using Moq;
using RccManager.Application.Mapper;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.Tests.Service.Services
{
    public class ParoquiaCapelaServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IParoquiaCapelaRepository> _repositoryMock;
        private readonly IParoquiaCapelaService _service;
        private readonly Mock<ICachingService> _cacheMock;

        public ParoquiaCapelaServiceTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new DtoToEntityProfile());
            }).CreateMapper();

            _repositoryMock = new Mock<IParoquiaCapelaRepository>();
            _cacheMock = new Mock<ICachingService>();
            _service = new ParoquiaCapelaService(_mapper, _repositoryMock.Object, _cacheMock.Object);
        }

        [Fact]
        public async Task Create_WithValidDto_ReturnsSuccessResponse()
        {
            // Arrange
            var dto = new ParoquiaCapelaDto { Address = "Test Address", Neighborhood = "Test Neighborhood" };
            var entity = _mapper.Map<ParoquiaCapela>(dto);

            _repositoryMock.Setup(repo => repo.Insert(It.IsAny<ParoquiaCapela>())).ReturnsAsync(entity);

            // Act
            var result = await _service.Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Paróquia/Capela criada com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
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
            Assert.Equal("Paróquia/Capela removida com sucesso.", result.Message);
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
            Assert.Equal("Houve um problema para remover a Paróquia/Capela", result.Message);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ActivateDeactivate_WithValidId_ReturnsSuccessResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new ParoquiaCapela { Id = id, Active = true };

            _repositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(entity);
            _repositoryMock.Setup(repo => repo.Update(It.IsAny<ParoquiaCapela>())).ReturnsAsync(entity);

            // Act
            var result = await _service.ActivateDeactivate(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Paróquia/Capela ativada/inativada com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.False(entity.Active);
        }

        [Fact]
        public async Task ActivateDeactivate_WithInvalidId_ReturnsErrorResponse()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync((ParoquiaCapela)null);

            // Act
            var result = await _service.ActivateDeactivate(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Paróquia/Capela não encontrada", result.Message);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GetAll_WithSearchTerm_ReturnsListOfDtoResults()
        {
            // Arrange
            var searchTerm = "SearchTerm";
            var entities = new List<ParoquiaCapela>
            {
                new ParoquiaCapela { Id = Guid.NewGuid(), Address = "Address 1", Neighborhood = "Neighborhood 1" },
                new ParoquiaCapela { Id = Guid.NewGuid(), Address = "Address 2", Neighborhood = "Neighborhood 2" },
                new ParoquiaCapela { Id = Guid.NewGuid(), Address = "Address 3", Neighborhood = "Neighborhood 3" }
            };

            _repositoryMock.Setup(repo => repo.GetAll(searchTerm)).ReturnsAsync(entities);

            // Act
            var result = await _service.GetAll(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ParoquiaCapelaDtoResult>>(result);
            Assert.Equal(entities.Count, result.Count());
        }

        [Fact]
        public async Task Update_WithValidDtoAndId_ReturnsSuccessResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ParoquiaCapelaDto { Address = "Updated Address", Neighborhood = "Updated Neighborhood" };
            var entity = _mapper.Map<ParoquiaCapela>(dto);

            _repositoryMock.Setup(repo => repo.Update(It.IsAny<ParoquiaCapela>())).ReturnsAsync(entity);
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(entity);

            // Act
            var result = await _service.Update(dto, id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Paróquia/Capela atualizada com sucesso.", result.Message);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
