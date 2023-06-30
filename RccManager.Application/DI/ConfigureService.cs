using Microsoft.Extensions.DependencyInjection;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

namespace RccManager.Application.DI
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection services)
        {
            services.AddTransient<IDecanatoSetorService, DecanatoSetorService>();
            services.AddTransient<IParoquiaCapelaService, ParoquiaCapelaService>();
        }
    }
}
