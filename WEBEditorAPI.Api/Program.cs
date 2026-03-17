using DotNetEnv;
using WEBEditorAPI.Api.Filters;
using WEBEditorAPI.Infrastructure;
using WEBEditorAPI.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

// Load correct .env
if (builder.Environment.IsDevelopment())
    Env.Load(".env.development");
else
    Env.Load(".env.production");
builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));

// Connect to the database
builder.Services.AddInfrastructure();

// Add health checks support
builder.Services.AddHealthChecks();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

var app = builder.Build();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
