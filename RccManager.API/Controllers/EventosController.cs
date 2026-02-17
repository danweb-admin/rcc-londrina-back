using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

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

        [HttpGet("get-all-home")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllHome()
        {
            var eventos = await _eventoService.GetAllHome();
            return Ok(eventos);
        }

        [HttpGet("lote-inscricao")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLoteInscricao(Guid eventoId)
        {
            try
            {
                var eventos = await _eventoService.LoteInscricao(eventoId);
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                 return BadRequest(new Models.ValidationResult { Code = "500", Message = ex.Message, PropertyName = ex.Source });

            }
            
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var eventos = await _eventoService.GetAll();
            return Ok(eventos);
        }

        [HttpGet("get-events-by-email")]
        public async Task<IActionResult> GetByEmailCheckin(string email)
        {
            var eventos = await _eventoService.GetEventsByEmail(email);
            return Ok(eventos);
        }

        [HttpGet("get-slug")]
        public async Task<IActionResult> GetSlug(string slug)
        {
            var evento = await _eventoService.GetSlug(slug);
            return Ok(evento);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var evento = await _eventoService.GetById(id);

            if (evento == null)
                return NotFound(new { message = "Evento não encontrado." });

            return Ok(evento);
        }

        [HttpGet("{eventoId}/campos")]
        public async Task<IActionResult> EventoCampos(Guid eventoId)
        {
            var camposFormularios = await _eventoService.GetCamposByEvento(eventoId);

            

            return Ok(camposFormularios);
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
        [AllowAnonymous]
        public async Task<IActionResult> Inscricao([FromBody] InscricaoDto inscricao)
        {
            try
            {
                var inscricao_ = await _eventoService.Inscricao(inscricao);

                Console.WriteLine($"Inscricao: {inscricao.Nome}, {inscricao.CodigoInscricao}");

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

        [HttpGet("reenvio-comprovante")]
        public async Task<IActionResult> InscricaoReenvioComprovante([FromQuery] string codigoInscricao, string email)
        {
            try
            {
                var inscricao_ = await _eventoService.ReenvioComprovante(codigoInscricao,email);

                return Ok(inscricao_);
            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
            catch (WebException ex)
            {

                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }

        }

        [HttpGet("verifica-status")]
        public async Task<IActionResult> VerificaStatus([FromQuery] string codigoInscricao)
        {
            try
            {
                var inscricao_ = await _eventoService.VerificaStatus(codigoInscricao);

                return Ok(new {
                    status = inscricao_
                });

            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
            catch (WebException ex)
            {

                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }

        }

        [HttpGet("{eventoId}/inscricoes")]
        public async Task<IActionResult> InscricoesCheckin(Guid eventoId)
        {
            try
            {
                var inscricoes = await _eventoService.GetAllInscricoesByEvento(eventoId);

                return Ok(inscricoes);

            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
            catch (WebException ex)
            {

                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }

        }

        [HttpGet("{codigoInscricao}/checkin")]
        public async Task<IActionResult> Checkin(string codigoInscricao)
        {
            try
            {

                var inscricoes = await _eventoService.FazerCheckin(codigoInscricao);

                return Ok(inscricoes);

            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
            catch (WebException ex)
            {

                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }

        }

        [HttpGet("isentar-inscricao")]
        public async Task<IActionResult> IsentarInscricao([FromQuery] string codigoInscricao)
        {
            try
            {
                var inscricao_ = await _eventoService.IsentarInscricao(codigoInscricao);

                return Ok(inscricao_);
            }
            catch (ValidateByNameException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
            catch (WebException ex)
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

