using System;
using Elastic.Apm.Api;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Interfaces.Services;

namespace RccManager.API.Controllers
{
    [Route("api/webhook")]
    public class WebhookController : ControllerBase
    {
        private readonly IEventoService  service;

        public WebhookController(IEventoService _service)
        {
            service = _service;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {

            try
            {
                // Lê o corpo bruto da requisição
                using var reader = new StreamReader(Request.Body);
                var body = await reader.ReadToEndAsync();

                // Pega o HMAC enviado no header
                var signatureHeader = Request.Headers;

                var receivedHmac = Request.Headers["HMAC"].ToString();

                Console.WriteLine(body);

                //if (string.IsNullOrEmpty(receivedHmac))
                //    return Unauthorized("Assinatura não encontrada");

                var result = await service.EventosWebhook(body);


                return Ok(new { status = "Processo atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok(new { status = "Processo atualizado com sucesso." });
            }
        }


    }
}

