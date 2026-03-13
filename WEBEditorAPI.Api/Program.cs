using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Domain.ValueObjects;
using WEBEditorAPI.Infrastructure;
using WEBEditorAPI.Infrastructure.Provider;

var builder = WebApplication.CreateBuilder(args);

// Load correct .env
if (builder.Environment.IsDevelopment())
    Env.Load(".env.development");
else
    Env.Load(".env.production");

// Create connection string
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASS")}";

// Connect to the database
builder.Services.AddInfrastructure(connectionString);

// Add health checks support
builder.Services.AddHealthChecks();

var app = builder.Build();

// map the official endpoint
app.MapHealthChecks("/health");

app.MapGet("/users", async ([FromServices] IUserRepository repository) =>
{
    var provider = new PBKDF2PasswordProvider();
    return await repository.GetAllAsync();
});

app.Run();
