using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nexora.Infrastructure.Options;
using Nexora.Infrastructure.Persistence;

namespace Nexora.Infrastructure.DI;

public static class DatabaseDI
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContextPool<PlatformDbContext>(ConfigureDbContext);
        services.AddDbContextPool<CulinaryDbContext>(ConfigureDbContext);

        return services;
    }
    private static void ConfigureDbContext(IServiceProvider sp, DbContextOptionsBuilder options)
    {
        var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
        var env = sp.GetRequiredService<IHostEnvironment>();
        if (env.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
        }
        options.UseNpgsql(dbOptions.ConnectionString, npgsql =>
        {
            npgsql.EnableRetryOnFailure();
            npgsql.CommandTimeout(30);
        });
    }
}
