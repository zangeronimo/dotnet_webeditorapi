using Microsoft.Extensions.DependencyInjection;

using Nexora.Application.Interfaces;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Infrastructure.Provider;
using Nexora.Infrastructure.Provider.StructuredData;

namespace Nexora.Infrastructure.DI;

public static class ProviderDI
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddSingleton<IMessageProvider, MessageProvider>();
        services.AddSingleton<IPasswordProvider, Argon2PasswordProvider>();
        services.AddSingleton<ITokenProvider, JwtProvider>();
        services.AddSingleton<IStorageProvider, DiskStorageProvider>();
        services.AddSingleton<ISecretGenerator, SecretGenerator>();
        services.AddSingleton<IEncryptionProvider, AesEncryptionProvider>();
        services.AddSingleton<IRecipeStructuredDataProvider, RecipeStructuredDataProvider>();

        return services;
    }
}
