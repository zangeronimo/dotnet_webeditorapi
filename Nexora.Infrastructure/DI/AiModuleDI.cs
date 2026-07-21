using Microsoft.Extensions.DependencyInjection;

using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.AI;
using Nexora.Application.UseCases.Culinary.Recipes;

namespace Nexora.Infrastructure.DI;

public static class AiModuleDI
{
    public static IServiceCollection AddAiModule(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<GenerateContentRequest, string>, GenerateContentUC>();

        return services;
    }
}
