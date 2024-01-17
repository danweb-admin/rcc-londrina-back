using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Exception.Servo;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/servos-temporarios")]
    //[Authorize]
    public class ServoTempController : ControllerBase
    {
        private readonly IServoTempService _servoService;

        public ServoTempController(IServoTempService servoService)
		{
            _servoService = servoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid grupoOracaoId)
        {
            var servos = await _servoService.GetAll(grupoOracaoId);
            return Ok(servos);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] ServoTempDto servoViewModel)
        {
            Console.WriteLine(".....SERVOS TEMPORARIOS.......");
            Console.WriteLine($"NOME: {servoViewModel.Name}");

            _servoService.Create(servoViewModel);

            return Ok(HttpStatusCode.Created);
            
        }

        [HttpPost("validado")]
        public async Task<IActionResult> Checked([FromBody] ServoTempDtoResult servoViewModel)
        {
            try
            {
                var result = await _servoService.Checked(servoViewModel.Id);

                if (result.StatusCode == 200)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (ValidateByCpfOrEmailException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ServoTempDto servoViewModel)
        {
            var updatedServo = await _servoService.Update(servoViewModel, id);

            if (updatedServo == null)
                return NotFound();

            return Ok(HttpStatusCode.NoContent);
        }
    }
}

