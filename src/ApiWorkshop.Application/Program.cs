using ApiWorkshop.Application.Data.Context;
using ApiWorkshop.Application.Domain.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ApiContext"),
    b => b.MigrationsHistoryTable("__EFMigrationsHistory", "workshop")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error;

    var result = new DefaultResponse<dynamic>
    {
        Message = exception?.Message ?? ""
    };

    context.Response.ContentType = "application/json";
    var code = 500;

    if (exception is HttpStatusException httpException)
    {
        code = (int)httpException.Status;
    }

    result.StatusCode = code;
    result.Success = false;
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

app.UseHealthChecks("/api/status");
app.UseAuthorization();

app.MapControllers();

app.Run();
