using Microsoft.Extensions.DependencyInjection;

namespace WEBEditorAPI.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabase();
        services.AddProviders();
        services.AddSystemModule();
        services.AddCulinaryModule();

        return services;
    }
}
