using Microsoft.Extensions.DependencyInjection;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.Application.DI
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection services)
        {
            services.AddScoped<IDecanatoSetorService, DecanatoSetorService>();
            services.AddScoped<IParoquiaCapelaService, ParoquiaCapelaService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGrupoOracaoService, GrupoOracaoService>();
            services.AddScoped<ICachingService, CachingService>();
            services.AddSingleton<IMD5Service, MD5Service>();

        }
    }
}
