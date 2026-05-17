using Microsoft.Extensions.DependencyInjection;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.UseCases.Culinary.Levels;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Infrastructure.Repositories.Culinary;

namespace Nexora.Infrastructure.DI;

public static class CulinaryModuleDI
{
    public static IServiceCollection AddCulinaryModule(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<GetByIdRequest, CategoryDto>, GetCategoryByIdUC>();

        // Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
