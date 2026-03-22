using System.Text;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using WEBEditorAPI.Api.Filters;
using WEBEditorAPI.Api.Middlewares;
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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "WEBEditorAPI",
            ValidateAudience = true,
            ValidAudience = "WEBEditor",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseAuthentication();
app.UseMiddleware<UserContextMiddleware>();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
