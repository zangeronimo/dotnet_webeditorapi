using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Infrastructure.Provider;

namespace WEBEditorAPI.Infrastructure.DI;

public static class ProviderDI
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordProvider, PBKDF2PasswordProvider>();
        services.AddSingleton<ITokenProvider, JwtProvider>();
        services.AddSingleton<IStorageProvider, DiskStorageProvider>();

        return services;
    }
}
