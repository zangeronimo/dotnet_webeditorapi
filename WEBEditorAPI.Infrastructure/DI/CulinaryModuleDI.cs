using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.UseCases.Culinary.Levels;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Repositories.Culinary;

namespace WEBEditorAPI.Infrastructure.DI;

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
