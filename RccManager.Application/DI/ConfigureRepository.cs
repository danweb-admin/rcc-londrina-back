using Microsoft.Extensions.DependencyInjection;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Repositories;

namespace RccManager.Application.DI
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IDecanatoSetorRepository, DecanatoSetorRepository>();
            services.AddScoped<IParoquiaCapelaRepository, ParoquiaCapelaRepsoitory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGrupoOracaoRepository, GrupoOracaoRepository>();
            services.AddScoped<IServoRepository, ServoRepository>();
        }
    }
}
