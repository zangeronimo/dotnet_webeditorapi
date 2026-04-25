using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs.JsonLd;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.JsonLd;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Infrastructure.Provider;
using WEBEditorAPI.Infrastructure.Provider.JsonLd;

namespace WEBEditorAPI.Infrastructure.DI;

public static class ProviderDI
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordProvider, Argon2PasswordProvider>();
        services.AddSingleton<ITokenProvider, JwtProvider>();
        services.AddSingleton<IStorageProvider, DiskStorageProvider>();
        services.AddSingleton<IJsonLdProvider<RecipeJsonLdRequest, RecipeJsonLd>, RecipeJsonLdProvider>();

        return services;
    }
}
