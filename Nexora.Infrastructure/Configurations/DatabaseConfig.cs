using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexora.Infrastructure.Persistence;

namespace Nexora.Infrastructure.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PlatformDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
}