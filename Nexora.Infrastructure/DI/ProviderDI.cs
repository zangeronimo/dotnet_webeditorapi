using Microsoft.Extensions.DependencyInjection;
using Nexora.Application.DTOs.JsonLd;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.JsonLd;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Infrastructure.Provider;
using Nexora.Infrastructure.Provider.JsonLd;

namespace Nexora.Infrastructure.DI;

public static class ProviderDI
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddSingleton<IMessageProvider, MessageProvider>();
        services.AddSingleton<IPasswordProvider, Argon2PasswordProvider>();
        services.AddSingleton<ITokenProvider, JwtProvider>();
        services.AddSingleton<IStorageProvider, DiskStorageProvider>();
        services.AddSingleton<IJsonLdProvider<RecipeJsonLdRequest, RecipeJsonLd>, RecipeJsonLdProvider>();

        return services;
    }
}
