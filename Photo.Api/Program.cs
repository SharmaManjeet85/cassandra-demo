using FluentValidation;
using FluentValidation.AspNetCore;
using Photo.Api.Extensions;
using Photo.Api.Middlewares;
using Photo.Application.Interfaces;
using Photo.Application.Services;
using Photo.Application.Validators;
using Serilog;
using Cassandra;
using Photo.Api.HealthChecks;
using Photo.Api.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] " +
        "[CorrelationId: {CorrelationId}] " +
        "{Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services
    .AddHealthChecks()
    .AddCheck<CassandraHealthCheck>(
        "Cassandra",
        failureStatus: null,
        tags: new[] { "db", "cassandra" });

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "frontend",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});        

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<
    CreatePhotoRequestValidator>();

builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.RegisterInfrastructure(
    builder.Configuration);

builder.Services.AddHostedService<CassandraStartupService>();

var app = builder.Build();

app.UseCors("frontend");
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.MapHealthChecks("/health");

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();