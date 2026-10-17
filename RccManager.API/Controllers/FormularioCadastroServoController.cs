
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.FormularioCadastroServo;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;

namespace RccManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/formularios-servo")]
    [AllowAnonymous]
    public class FormularioCadastroServoController : ControllerBase
    {
        private readonly IServoTempService _servoTempService;
        private readonly IFormularioCadastroServoRepository _formularioRepository;

        public FormularioCadastroServoController(
            IServoTempService servoTempService,
            IFormularioCadastroServoRepository formularioRepository)
        {
            _servoTempService = servoTempService;
            _formularioRepository = formularioRepository;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> Get(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest(new { message = "Token inválido." });

            var formulario =
                await _formularioRepository.GetByToken(token);

            if (formulario == null || !formulario.Active)
                return NotFound(new
                {
                    message = "Formulário inexistente ou inativo."
                });

            return Ok(
                formulario
            );
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> Post(
            string token,
            [FromBody] CadastroServoFormularioDto cadastro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(token))
                return BadRequest(new { message = "Token inválido." });

            if (!cadastro.AceitouConsentimento)
                return BadRequest(new
                {
                    message = "É necessário aceitar o termo de consentimento."
                });

            var formulario =
                await _formularioRepository.GetByToken(token);

            if (formulario == null || !formulario.Active)
                return NotFound(new
                {
                    message = "Formulário inexistente ou inativo."
                });

            var servo = new ServoTempDto
            {
                Name = cadastro.Name?.Trim(),
                Birthday = cadastro.Birthday,
                Cpf = cadastro.Cpf,
                Email = cadastro.Email?.Trim(),
                CellPhone = cadastro.CellPhone?.Trim(),
                MainMinistry = cadastro.MainMinistry,
                SecondaryMinistry = cadastro.SecondaryMinistry,

                // Definidos pelo backend.
                GrupoOracaoId = formulario.GrupoOracaoId,
                Active = true,
                Checked = false
            };

            var resultado = await _servoTempService.Create(servo);

            if (resultado == null)
                return StatusCode(500, new
                {
                    message = "Não foi possível registrar o cadastro."
                });

            if (resultado.StatusCode < 200 ||
                resultado.StatusCode >= 300)
            {
                return BadRequest(new
                {
                    message = resultado.Message
                });
            }

            return Ok(new
            {
                message = "Cadastro enviado com sucesso."
            });
        }
    }
}
