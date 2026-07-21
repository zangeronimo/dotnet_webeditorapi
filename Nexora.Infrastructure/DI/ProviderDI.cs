using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Nexora.Application.Interfaces;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Infrastructure.Options;
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

        services.AddHttpClient<IAiProvider, GeminiProvider>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<AiOptions>>().Value;

            client.BaseAddress = new Uri(options.Gemini.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(60);
        });

        return services;
    }
}
