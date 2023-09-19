﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Dtos.Users;
using System.ComponentModel.DataAnnotations;

namespace RccManager.API.Controllers;

[Route("api/v1")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly ILogger<UsersController> logger;


    public UsersController(IUserService _userService, ITokenService _tokenService, ILogger<UsersController> _logger)
    {
        userService = _userService;
        tokenService = _tokenService;
        logger = _logger;
    }


    [HttpGet("user")]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserDtoResult))]
    public async Task<IEnumerable<UserDtoResult>> GetAllAsync()
    {
        logger.LogInformation($"{nameof(GetAllAsync)} | Inicio da chamada");
        return await userService.GetAll();
    }

    [HttpPost("user")]
    [AllowAnonymous]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
    public async Task<IActionResult> PostAsync([FromBody] UserDtoAdd model)
    {
        logger.LogInformation($"{nameof(PostAsync)} | Inicio da chamada - {model.Email}");
        model.Active = false;

        var result = await userService.Add(model);

        if (result.StatusCode != 200)
        {
            logger.LogWarning($"{nameof(PostAsync)} | Erro na criacao - {model.Email} - {result}");
            return NotFound(result);
        }
        return Ok(result);
    }


    [HttpPut("user/{id}")]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] UserDto model)
    {
        logger.LogInformation($"{nameof(PutAsync)} | Inicio da chamada - {model.Email}");
        var result = await userService.Update(model, id);

        if (result != null)
        {
            logger.LogWarning($"{nameof(PutAsync)} | Erro na atualizacao - {model.Email} - {result}");
            return NotFound(result);

        }
        return Ok(result);

    }

    [HttpPost("user/change-user-password")]
    [AllowAnonymous]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
    
    public async Task<IActionResult> ChangeUserPassworAsync([FromBody] UserDtoChangePassword model)
    {
        logger.LogInformation($"{nameof(ChangeUserPassworAsync)} | Inicio da chamada - {model.Email}");
        // Recupera o usuário

        var userAuthenticated = await userService.GetByName(User.Identity.Name);

        var user = await userService.GetByEmail(model.Email);

        if (userAuthenticated.Name != user.Name)
        {
            logger.LogWarning($"{nameof(ChangeUserPassworAsync)} | Erro Autenticacao - {model.Email}");
            return BadRequest(new Domain.Responses.HttpResponse { StatusCode = 404, Message = "Você não pode alterar a senha de outro usuário." });
        }

        if (user == null)
        {
            logger.LogWarning($"{nameof(ChangeUserPassworAsync)} | Erro Autenticacao - {model.Email}");
            return BadRequest(new Domain.Responses.HttpResponse { StatusCode = 404, Message = "Usuário não encontrado." });

        }

        return Ok(await userService.ChangeUserPassword(user, model.Password));
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] UserDtoLogin model)
    {
        try
        {
            logger.LogInformation($"{nameof(Authenticate)} | Inicio da chamada - {model.Email}");

            var user = await userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                logger.LogWarning($"{nameof(Authenticate)} | Erro Autenticacao - {model.Email}");
                return BadRequest(new Domain.Responses.HttpResponse { StatusCode = 400, Message = "Senha ou usuário inválido." });

            }

            var token = tokenService.GenerateToken(user);

            return Ok(new
            {
                user,
                token
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Domain.Responses.HttpResponse { StatusCode = 400, Message = ex.Message });

        }

    }
}

