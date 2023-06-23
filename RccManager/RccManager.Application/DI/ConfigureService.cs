using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Infra.Context;
using RccManager.Infra.Repositories;
using RccManager.Service.Services;
using RccManager.Service.Validators.DecanatoSetor;

namespace RccManager.Application.DI;

public class ConfigureService
{
	public static void ConfigureDependenciesService(IServiceCollection services)
	{
		services.AddTransient<IDecanatoSetorService,DecanatoSetorService>();
		services.AddScoped<IValidator<DecanatoSetorDto>, DecanatoSetorValidator>();
    }
}

