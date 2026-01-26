using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.FormacoesServo;
using RccManager.Domain.Dtos.TransferenciaServico;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Services;

namespace RccManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/transferencia-servo")]
    [Authorize]
    public class TransferenciaServosController : ControllerBase
    {
        private readonly ITransferenciaServoService _service;
                private readonly IUserService _userService;


        public TransferenciaServosController(ITransferenciaServoService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid servoId)
        {
        
            var transferencias = await _service.GetAll();
            return Ok(transferencias);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransferenciaServoDto viewModel)
        {
            try
            {
                var user = await _userService.GetUserContext(User);

                var createdFormacao = await _service.Create(viewModel, user);

                return Ok(HttpStatusCode.Created);
            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }catch(WebException ex)
            {
               
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }


        }
    }
}

