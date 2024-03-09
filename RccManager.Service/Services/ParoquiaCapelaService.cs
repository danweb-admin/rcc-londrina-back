using System.Net;
using AutoMapper;
using Newtonsoft.Json;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Enum;

namespace RccManager.Service.Services;

public class ParoquiaCapelaService : IParoquiaCapelaService
{
    private readonly IMapper mapper;
    private readonly IParoquiaCapelaRepository repository;
    private readonly ICachingService cache;
    private readonly string hashParoquiaCapela = "paroquia-capela";
    private readonly IHistoryRepository history;


    public ParoquiaCapelaService(IMapper _mapper, IParoquiaCapelaRepository _repository, ICachingService _cache, IHistoryRepository _history)
    {
        mapper = _mapper;
        repository = _repository;
        cache = _cache;
        history = _history;
        
    }

    public async Task<HttpResponse> Create(ParoquiaCapelaDto paroquiaCapelaDto)
    {

        var paroquiaCapela = mapper.Map<ParoquiaCapela>(paroquiaCapelaDto);
        var result = await repository.Insert(paroquiaCapela);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para criar a Paróquia/Capela", StatusCode = (int)HttpStatusCode.BadRequest };

        await cache.SetAsync(hashParoquiaCapela, result.Id.ToString(), JsonConvert.SerializeObject(result));

        // adiciona a tabela de histórico de alteracao
        await history.Add(TableEnum.DecanatoSetor.ToString(), result.Id, OperationEnum.Criacao.ToString());

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

        if (entity == null)
            return new HttpResponse { Message = "Paróquia/Capela não encontrada", StatusCode = (int)HttpStatusCode.BadRequest };

        entity.Active = !entity.Active;

        var result = await repository.Update(entity);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para ativar/inativar a Paróquia/Capela", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Paróquia/Capela ativada/inativada com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<IEnumerable<ParoquiaCapelaDtoResult>> GetAll(string search)
    {
        var cachedEntities = await cache.GetAllAsync(hashParoquiaCapela);
        IEnumerable<ParoquiaCapela> entities;

        if (cachedEntities.Length == 0)
        {
            entities = await repository.GetAll(search);
            await CachingParoquiaCapela();
            return mapper.Map<IEnumerable<ParoquiaCapelaDtoResult>>(entities);
        }

        
        return mapper.Map<IEnumerable<ParoquiaCapelaDtoResult>>(GetAllCache(search, cachedEntities));
    }

    public async Task<HttpResponse> Update(ParoquiaCapelaDto paroquiaCapelaDto, Guid id)
    {

        var paroquiaCapela = mapper.Map<ParoquiaCapela>(paroquiaCapelaDto);
        paroquiaCapela.Id = id;

        var result = await repository.Update(paroquiaCapela);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

        var result_ = await repository.GetById(result.Id);
        await cache.SetAsync(hashParoquiaCapela, result_.Id.ToString(), JsonConvert.SerializeObject(result_));

        // adiciona a tabela de histórico de alteracao
        await history.Add(TableEnum.DecanatoSetor.ToString(), result.Id, OperationEnum.Criacao.ToString());

        return new HttpResponse { Message = "Paróquia/Capela atualizada com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<HttpResponse> CachingParoquiaCapela()
    {
        var list = await repository.GetAll(string.Empty);
        
        foreach (var item in list)
        {
            var paroquiaCapela = await cache.GetAsync(hashParoquiaCapela,item.Id.ToString());

            if (paroquiaCapela == null)
            {
                await cache.SetAsync(hashParoquiaCapela, item.Id.ToString(), JsonConvert.SerializeObject(item));
            }
        }

        return new HttpResponse { Message = "Paróquia/Capela foram cacheados.", StatusCode = (int)HttpStatusCode.OK };

    }

    private IEnumerable<ParoquiaCapela> GetAllCache(string search, StackExchange.Redis.HashEntry[] cachedEntities)
    {
        search = search.ToUpper();

        var entities = cachedEntities.Select(x => JsonConvert.DeserializeObject<ParoquiaCapela>((string)x.Value)).ToList();
        return entities.Where(
                x => x.Name.Contains(search) ||
                x.Address.Contains(search) ||
                x.Neighborhood.Contains(search) ||
                x.DecanatoSetor.Name.Contains(search))
            .OrderBy(x => x.Name);
    }
}