using System.Globalization;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Nexora.Api.Authorization;
using Nexora.Api.Filters;
using Nexora.Api.Middlewares;
using Nexora.Domain.Config;
using Nexora.Infrastructure.DI;
using Nexora.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

// Load correct .env
if (builder.Environment.IsDevelopment())
    Env.Load(".env.development");
else
    Env.Load(".env.production");
builder.Configuration.AddEnvironmentVariables();

// --------------------
// CORS
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });

    options.AddPolicy("ProdCors", policy =>
    {
        policy.WithOrigins(
                "https://nexora-api.tudolinux.com.br"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Location (i18n)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("API"));
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
            ValidIssuer = "Nexora",
            ValidateAudience = true,
            ValidAudience = "Nexora",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
var env = app.Environment;
if (env.IsProduction())
{
    app.UseCors("ProdCors");
}
else
{
    app.UseCors("DevCors");
}

// --------------------
// Static Files (/files)
// --------------------
if (!env.IsProduction())
{
    var uploadPath = Path.Combine(env.ContentRootPath, "upload");

    // garante que a pasta existe
    Directory.CreateDirectory(uploadPath);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(uploadPath),
        RequestPath = "/files"
    });
}

// Location (i18n)
var supportedCultures = LocalizationConfig.SupportedCultures
    .Select(c => new CultureInfo(c))
    .ToList();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserContextMiddleware>();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
