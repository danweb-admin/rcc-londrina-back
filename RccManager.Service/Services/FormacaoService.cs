using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Entities;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;

namespace RccManager.Service.Services;

public class FormacaoService : IFormacaoService
{
    private readonly IMapper mapper;
    private IFormacaoRepository repository;

    public FormacaoService(IMapper _mapper, IFormacaoRepository _repository)
	{
        this.mapper = _mapper;
        this.repository = _repository;
    }

    public async Task<HttpResponse> Create(FormacaoDto formacaoViewModel)
    {

        if (await repository.GetByName(formacaoViewModel.Name))
            throw new ValidateByNameException("ERRO: Essa formação já existe.");

        var formacao = mapper.Map<Formacao>(formacaoViewModel);



        var result = await repository.Insert(formacao);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para criar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Objeto criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<HttpResponse> Delete(Guid id)
    {
        var result = await repository.Delete(id);

        if (result)
            return new HttpResponse { Message = "Objeto removido com sucesso.", StatusCode = (int)HttpStatusCode.OK };

        return new HttpResponse { Message = "Houve um problema para remover o objeto", StatusCode = (int)HttpStatusCode.BadRequest };
    }

    public async Task<HttpResponse> ActivateDeactivate(Guid id)
    {
        var entity = await repository.GetById(id);
        entity.Active = !entity.Active;

        var result = await repository.Update(entity);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para ativar/inativar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Objeto ativado/inativado com sucesso.", StatusCode = (int)HttpStatusCode.OK };

    }

    public async Task<IEnumerable<FormacaoDtoResult>> GetAll(bool active)
    {
        return mapper.Map<IEnumerable<FormacaoDtoResult>>(await repository.GetAll(active));
    }

    public async Task<HttpResponse> Update(FormacaoDto formacaoViewModel, Guid id)
    {
        if (await repository.GetByName(formacaoViewModel.Name, id))
            throw new ValidateByNameException("ERRO: Essa formação já existe.");

        var formacao = mapper.Map<Formacao>(formacaoViewModel);
        formacao.Id = id;

        var result = await repository.Update(formacao);

        if (result == null)
            return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

        return new HttpResponse { Message = "Objeto atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
    }

    public async Task<IEnumerable<FormacaoDtoResult>> GetAll()
    {
        return mapper.Map<IEnumerable<FormacaoDtoResult>>(await repository.GetAll(string.Empty));

    }
}

