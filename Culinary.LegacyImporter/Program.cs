using Culinary.LegacyImporter;

using Microsoft.EntityFrameworkCore;

using Nexora.Infrastructure.Persistence;
var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<LegacyDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Legacy")));

builder.Services.AddDbContext<CulinaryDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Current")));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
