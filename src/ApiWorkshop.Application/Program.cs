using ApiWorkshop.Application.Data.Context;
using ApiWorkshop.Application.Data.Repositories;
using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Extensions;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Utils;
using ApiWorkshop.Application.Middlewares;
using ApiWorkshop.Application.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddCors();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services
    .AddScoped<IBaseRepository<PrizeDraw>, BaseRepository<PrizeDraw>>()
    .AddScoped<IBaseRepository<Gift>, BaseRepository<Gift>>()
    .AddScoped<IBaseRepository<Draw>, BaseRepository<Draw>>()
    .AddScoped<IBaseRepository<Entrant>, BaseRepository<Entrant>>()
    .AddScoped<IGiftService, GiftService>()
    .AddScoped<IDrawService, DrawService>()
    .AddScoped<IEntrantService, EntrantService>()
    .AddScoped<IPrizeDrawService, PrizeDrawService>();

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddHealthChecks();

string connectionString =
    connectionString = Environment.GetEnvironmentVariable("CONTEXT") ?? builder.Configuration.GetConnectionString("ApiContext");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connectionString,
    b => b.MigrationsHistoryTable("__EFMigrationsHistory", "workshop")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error;

    var result = new BaseErrorResponse()
    {
        Errors = new List<string>() {
            exception?.Message ?? ""
    }
    };

    context.Response.ContentType = "application/json";
    var code = 500;

    if (exception is HttpStatusException httpException)
    {
        code = (int)httpException.Status;
    }

    context.Response.StatusCode = code;

    if (code != 204)
    {
        await context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
}));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

app.UseRequestMigrations();

app.UseHealthChecks("/api/status");
app.UseAuthorization();

app.MapControllers();

app.Run();
