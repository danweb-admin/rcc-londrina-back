using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.API.Controllers;

[ApiController]
[Route("api/v1/formacao")]
[Authorize]
public class FormacaoController : ControllerBase
{
    private readonly IFormacaoService _formacaoService;

    public FormacaoController(IFormacaoService formacaoService)
	{
        _formacaoService = formacaoService;
	}

    [HttpGet]
    public async Task<IActionResult> Get(bool active)
    {
        var formacoes = await _formacaoService.GetAll(active);
        return Ok(formacoes);
    }


    [HttpGet("get-all")]
    public async Task<IActionResult> Get()
    {
        var formacoes = await _formacaoService.GetAll();
        return Ok(formacoes);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FormacaoDto formacaoViewModel)
    {
        try
        {

            var createdFormacao = await _formacaoService.Create(formacaoViewModel);

            return Ok(HttpStatusCode.Created);
        }
        catch (ValidateByNameException ex)
        {
            return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] FormacaoDto formacaoViewModel)
    {
        try
        {
            var updatedFormacao = await _formacaoService.Update(formacaoViewModel, id);

            if (updatedFormacao == null)
                return NotFound();

            return Ok(HttpStatusCode.NoContent);
        }
        catch (ValidateByNameException ex)
        {
            return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedFormacao = await _formacaoService.ActivateDeactivate(id);

        if (deletedFormacao == null)
            return NotFound();

        return NoContent();
    }
}

