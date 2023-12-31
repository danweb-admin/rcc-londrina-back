﻿using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Exception.Decanato;
using RccManager.Domain.Exception.Servo;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/servos")]
    [Authorize]
    public class ServosController : ControllerBase
    {
        private readonly IServoService _servoService;


        public ServosController(IServoService servoService)
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
        public async Task<IActionResult> Post([FromBody] ServoDto servoViewModel)
        {
            try
            {

                var createdServo = await _servoService.Create(servoViewModel);

                return Ok(HttpStatusCode.Created);
            }
            catch (ValidateByCpfOrEmailException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }

        }

        [HttpPost("servos-google-forms")]
        [AllowAnonymous]
        public async Task<IActionResult> PostForms([FromBody] ServoFormsDto servoViewModel)
        {


            Console.WriteLine("-*****Servo Google Forms******-" );
            Console.WriteLine($"Nome: {servoViewModel.Nome}" );
            Console.WriteLine($"Email: {servoViewModel.Email}");
            Console.WriteLine($"Celular: {servoViewModel.Celular}");
            Console.WriteLine("***************************");

            return Ok(HttpStatusCode.Created);
            

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ServoDto servoViewModel)
        {
            try
            {
                var updatedServo = await _servoService.Update(servoViewModel, id);

                if (updatedServo == null)
                    return NotFound();

                return Ok(HttpStatusCode.NoContent);
            }
            catch (ValidateByCpfOrEmailException ex)
            {
                return BadRequest(new Models.ValidationResult { Code = "400", Message = ex.Message, PropertyName = ex.Source });
            }
        }
    }
}

