using Microsoft.OpenApi.Models;
using RccManager.Application.DI;
using RccManager.Application.Mapper;
using AutoMapper;
using RccManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using RccManager.Service.MQ;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Services;

var builder = WebApplication.CreateBuilder(args);

var redisHost = Environment.GetEnvironmentVariable("RedisHost");
var redisPort = Environment.GetEnvironmentVariable("RedisPort");

builder.Services.AddStackExchangeRedisCache(o =>
{
    o.InstanceName = "instance";
    o.Configuration = $"{redisHost}:{redisPort}";
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddFluentValidation(options =>
    {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RCCManager.API",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AppCors", policy =>
    {
        policy.WithOrigins(
                "https://eventos.rcc-londrina.online",
                "http://localhost:4200",
                "http://161.35.255.131:32597/"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);
ConfigureAppDbContext(builder);

// AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToEntityProfile());
    cfg.AddProfile(new EntityToDtoProfile());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var configurationKey = Environment.GetEnvironmentVariable("KeyMD5");
var key = Encoding.ASCII.GetBytes(configurationKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(m =>
{
    m.SlidingExpiration = true;
    m.ExpireTimeSpan = TimeSpan.FromMinutes(120);
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EmailQueueProducer>();

var UrlAsaas = Environment.GetEnvironmentVariable("UrlAsaas");
var AccessToken = Environment.GetEnvironmentVariable("AccessToken");

builder.Services.AddHttpClient("asaas", (sp, client) =>
{
    client.BaseAddress = new Uri(UrlAsaas);
    client.DefaultRequestHeaders.Add("access_token", AccessToken);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Background service
builder.Services.AddHostedService<RabbitMQEmailConsumer>();

builder.Services.AddScoped<IAsaasClient, AsaasClient>();
builder.Services.AddScoped<IPagamentoAsaasService, PagamentoAsaasService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RccManager.API v1"));

app.UseHttpsRedirection();

app.UseCors("AppCors");

app.Use(async (context, next) =>
{
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// ----------------------------------------------
//       ConfigureAppDbContext
// ----------------------------------------------

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
        connectionString =
            $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
        options.EnableSensitiveDataLogging();
        // options.EnableDetailedErrors(false);
        // options.EnableSensitiveDataLogging(false);
        // options.ConfigureWarnings(w =>
        //     w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuted));
    });
}
