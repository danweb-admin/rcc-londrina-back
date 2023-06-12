using System;
using AutoMapper;

namespace RccManager.API.Configuration;

public static class AutoMapperConfig
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));


        //services.AddAutoMapper(typeof(EntityToViewModelMappingProfile), typeof(ViewModelToEntityMappingProfile));
    }
}

