using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RccManager.API.Models;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Validators.DecanatoSetor;

namespace RccManager.API.Controllers;

[ApiController]
[Route("api/v1/decanato-setor")]
public class DecanatoSetorController : ControllerBase
{
    private readonly IDecanatoSetorService _decanatoSetorService;
    private readonly DecanatoSetorValidator _validator;

    public DecanatoSetorController(IDecanatoSetorService decanatoSetorService, DecanatoSetorValidator validator)
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
        try
        {
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(decanatoSetorViewModel);

            var createdDecanatoSetor = await _decanatoSetorService.Create(decanatoSetorViewModel);

            return Ok(HttpStatusCode.Created);
        }
        catch (ValidateByNameException ex)
        {
            return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] DecanatoSetorDto decanatoSetorViewModel)
    {
        try
        {
            var updatedDecanatoSetor = await _decanatoSetorService.Update(decanatoSetorViewModel, id);

            if (updatedDecanatoSetor == null)
                return NotFound();

            return Ok(HttpStatusCode.NoContent);
        }catch(ValidateByNameException ex)
        {
            return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedDecanatoSetor = await _decanatoSetorService.ActivateDeactivate(id);

        if (deletedDecanatoSetor == null)
            return NotFound();

        return NoContent();
    }

    
}
