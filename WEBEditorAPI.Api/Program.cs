using System.Text;
using DotNetEnv;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using WEBEditorAPI.Api.Filters;
using WEBEditorAPI.Api.Middlewares;
using WEBEditorAPI.Infrastructure.DI;
using WEBEditorAPI.Infrastructure.Options;

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
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    options.AddPolicy("ProdCors", policy =>
    {
        policy.WithOrigins(
                "https://webeditor2.tudolinux.com.br"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
app.UseAuthentication();
app.UseMiddleware<UserContextMiddleware>();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
