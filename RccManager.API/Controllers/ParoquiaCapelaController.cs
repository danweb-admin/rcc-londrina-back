using System.Net;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Interfaces.Services;

namespace RccManager.API.Controllers;

[ApiController]
[Route("api/v1/paroquia-capela")]
public class ParoquiaCapelaController : ControllerBase
{
    private readonly IParoquiaCapelaService _paroquiaCapelaService;

    public ParoquiaCapelaController(IParoquiaCapelaService paroquiaCapelaService)
    {
        _paroquiaCapelaService = paroquiaCapelaService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string search)
    {
        var paroquiaCapelas = await _paroquiaCapelaService.GetAll(search);
        return Ok(paroquiaCapelas);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ParoquiaCapelaDto paroquiaCapelaDto)
    {
        
        await _paroquiaCapelaService.Create(paroquiaCapelaDto);
        return Ok(HttpStatusCode.Created);
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] ParoquiaCapelaDto paroquiaCapelaDto)
    {
        
        var updatedParoquiaCapela = await _paroquiaCapelaService.Update(paroquiaCapelaDto, id);
        if (updatedParoquiaCapela == null)
            return NotFound();

        return Ok(HttpStatusCode.NoContent);
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedParoquiaCapela = await _paroquiaCapelaService.Delete(id);
        if (deletedParoquiaCapela == null)
            return NotFound();

        return NoContent();
    }
}
