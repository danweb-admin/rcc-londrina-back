using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;

namespace RccManager.Service.Services;

public class ParoquiaCapelaService : IParoquiaCapelaService
{
    private readonly IMapper mapper;
    private readonly IParoquiaCapelaRepository repository;

    public ParoquiaCapelaService(IMapper mapper, IParoquiaCapelaRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<HttpResponse> Create(ParoquiaCapelaDto paroquiaCapelaDto)
    {

        var paroquiaCapela = mapper.Map<ParoquiaCapela>(paroquiaCapelaDto);
        var result = await repository.Insert(paroquiaCapela);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para criar a Paróquia/Capela", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Paróquia/Capela criada com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<HttpResponse> Delete(Guid id)
    {
        var result = await repository.Delete(id);

        if (result)
            return new HttpResponse { Message = "Paróquia/Capela removida com sucesso.", StatusCode = (int)HttpStatusCode.OK };

        return new HttpResponse { Message = "Houve um problema para remover a Paróquia/Capela", StatusCode = (int)HttpStatusCode.BadRequest };
    }

    public async Task<HttpResponse> ActivateDeactivate(Guid id)
    {
        var entity = await repository.GetById(id);
        entity.Active = !entity.Active;

        var result = await repository.Update(entity);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para ativar/inativar a Paróquia/Capela", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Paróquia/Capela ativada/inativada com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<IEnumerable<ParoquiaCapelaDtoResult>> GetAll(string search)
    {
        var entities = await repository.GetAll(search);
        return mapper.Map<IEnumerable<ParoquiaCapelaDtoResult>>(entities);
    }

    public async Task<HttpResponse> Update(ParoquiaCapelaDto paroquiaCapelaDto, Guid id)
    {

        var paroquiaCapela = mapper.Map<ParoquiaCapela>(paroquiaCapelaDto);
        paroquiaCapela.Id = id;

        var result = await repository.Update(paroquiaCapela);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Paróquia/Capela atualizada com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }
}