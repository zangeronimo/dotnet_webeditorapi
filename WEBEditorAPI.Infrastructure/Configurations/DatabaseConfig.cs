using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
}