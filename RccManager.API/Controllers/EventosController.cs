using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Services;

namespace RccManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/eventos")]
    //[Authorize]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool active)
        {
            var eventos = await _eventoService.GetAll(active);
            return Ok(eventos);
        }

        [HttpGet("lote-inscricao")]
        public async Task<IActionResult> GetLoteInscricao(Guid eventoId)
        {
            var eventos = await _eventoService.LoteInscricao(eventoId);
            return Ok(eventos);
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var eventos = await _eventoService.GetAll(true);
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var evento = await _eventoService.GetById(id);

            if (evento == null)
                return NotFound(new { message = "Evento não encontrado." });

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventoDto eventoViewModel)
        {
            try
            {

                var createdFormacao = await _eventoService.Create(eventoViewModel);

                return Ok(HttpStatusCode.Created);
            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }

        }

        [HttpPost("inscricao")]
        public async Task<IActionResult> Inscricao([FromBody] InscricaoDto inscricao)
        {
            try
            {
                var inscricao_ = await _eventoService.Inscricao(inscricao);

                return Ok(inscricao_);
            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }catch(WebException ex)
            {
               
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] EventoDto eventoViewModel)
        {
            try
            {
                var updatedFormacao = await _eventoService.Update(eventoViewModel, id);

                if (updatedFormacao == null)
                    return NotFound();

                return Ok(HttpStatusCode.NoContent);
            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
        }

        
    }
}

