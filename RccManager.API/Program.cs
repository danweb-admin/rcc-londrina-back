using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RccManager.Application.DI;
using RccManager.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using RccManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddFluentValidation(options =>
        {
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    );

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RCCManager.API", Version = "v1" });
});


ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);
ConfigureAppDbContext(builder);

// Setup AutoMapper e dependency injection
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToEntityProfile());
    cfg.AddProfile(new EntityToDtoProfile());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

using (var serviceScope = app.Services.CreateScope())
{ 
    serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());


app.Use(async (context, next) =>
{
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureAppDbContext(WebApplicationBuilder builder)
{
    var server = Environment.GetEnvironmentVariable("DbServer");
    var port = Environment.GetEnvironmentVariable("DbPort");
    var user = Environment.GetEnvironmentVariable("DbUser");
    var password = Environment.GetEnvironmentVariable("Password");
    var database = Environment.GetEnvironmentVariable("Database");
    var development = Environment.GetEnvironmentVariable("Development");
    var connectionString = string.Empty;

    if (development == "True")
        connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SolucaoDB;";
    else
        connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}