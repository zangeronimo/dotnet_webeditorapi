using Microsoft.Extensions.DependencyInjection;

namespace Nexora.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabase();
        services.AddProviders();
        services.AddPlatformModule();
        services.AddCulinaryModule();

        return services;
    }
}
