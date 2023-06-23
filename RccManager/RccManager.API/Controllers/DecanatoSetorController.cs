using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RccManager.API.Models;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Interfaces;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;

namespace RccManager.API.Controllers;

[ApiController]
[Route("api/v1/decanato-setor")]
public class DecanatoSetorController : ControllerBase
{
    private readonly IDecanatoSetorService _decanatoSetorService;
    private readonly IValidator<DecanatoSetorDto> _validator;

    public DecanatoSetorController(IDecanatoSetorService decanatoSetorService, IValidator<DecanatoSetorDto> validator)
    {
        _decanatoSetorService = decanatoSetorService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var decanatoSetores = await _decanatoSetorService.GetAll();
        return Ok(decanatoSetores);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DecanatoSetorDto decanatoSetorViewModel)
    {
        var validation = await ValidateModel(decanatoSetorViewModel);

        if (validation != null)
            return BadRequest(validation);

        var createdDecanatoSetor = await _decanatoSetorService.Create(decanatoSetorViewModel);

        return Ok(HttpStatusCode.Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] DecanatoSetorDto decanatoSetorViewModel)
    {
        var validation = await ValidateModel(decanatoSetorViewModel);

        if (validation != null)
            return BadRequest(validation);

        var updatedDecanatoSetor = await _decanatoSetorService.Update(decanatoSetorViewModel, id);

        if (updatedDecanatoSetor == null)
            return NotFound();

        return Ok(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedDecanatoSetor = await _decanatoSetorService.ActivateDeactivate(id);

        if (deletedDecanatoSetor == null)
            return NotFound();

        return NoContent();
    }

    private async Task<IEnumerable<ValidationResult>> ValidateModel(DecanatoSetorDto model)
    {
        var validation = await _validator.ValidateAsync(model);

        if (!validation.IsValid)
        {
            return validation.Errors?.Select(e => new ValidationResult()
            {
                Code = e.ErrorCode,
                PropertyName = e.PropertyName,
                Message = e.ErrorMessage
            });
        }
        return null;
    }
}
