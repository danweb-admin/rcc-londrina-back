using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Dtos.FormacoesServo;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.API.Controllers;

[ApiController]
[Route("api/v1/formacoes-servo")]
[Authorize]
public class FormacoesServoController : ControllerBase
{
    private readonly IFormacoesServoService _formacoesServoService;
    private readonly IUserService _userService;

    public FormacoesServoController(IFormacoesServoService formacoesServoService, IUserService userService)
		{
        _formacoesServoService = formacoesServoService;
        _userService = userService;
		}

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid servoId)
    {
        
        var formacoesServos = await _formacoesServoService.GetAll(servoId);
        return Ok(formacoesServos);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FormacoesServoDtoCreate viewModel)
    {
        try
        {
            var user = await _userService.GetUserContext(User);
            viewModel.User = user;

            var createdFormacao = await _formacoesServoService.Create(viewModel);

            return Ok(HttpStatusCode.Created);
        }
        catch (ValidateByNameException ex)
        {
            return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedFormacaoServo = await _formacoesServoService.Delete(id);
        if (deletedFormacaoServo == null)
            return NotFound();

        return NoContent();
    }
}

