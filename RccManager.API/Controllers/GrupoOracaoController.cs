using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/grupo-oracao")]
    [Authorize]
    public class GrupoOracaoController : ControllerBase
    {
        private readonly IGrupoOracaoService _grupoOracaoService;

        public GrupoOracaoController(IGrupoOracaoService grupoOracaoService)
        {
            _grupoOracaoService = grupoOracaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? search)
        {
            string _search = string.Empty;
            if (search != null)
                _search = search;

            var grupoOracoes = await _grupoOracaoService.GetAll(_search);
            return Ok(grupoOracoes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GrupoOracaoDto grupoOracaoDto)
        {

            await _grupoOracaoService.Create(grupoOracaoDto);
            return Ok(HttpStatusCode.Created);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] GrupoOracaoDto grupoOracaoDto)
        {

            var updatedGrupoOracao = await _grupoOracaoService.Update(grupoOracaoDto, id);
            if (updatedGrupoOracao == null)
                return NotFound();

            return Ok(HttpStatusCode.NoContent);

        }
    }
}

