using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RccManager.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling =
                  Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RCCManager.API", Version = "v1" });
});

// .NET Native DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();

// Auto Mapper
builder.Services.AddAutoMapperConfiguration();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

