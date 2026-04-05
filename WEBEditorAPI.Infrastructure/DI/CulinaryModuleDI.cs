using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Categories;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Application.UseCases.Culinary.Categories;
using WEBEditorAPI.Application.UseCases.Culinary.Levels;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Repositories.Culinary;
using WEBEditorAPI.Infrastructure.Repositories.System;

namespace WEBEditorAPI.Infrastructure.DI;

public static class CulinaryModuleDI
{
    public static IServiceCollection AddCulinaryModule(this IServiceCollection services)
    {
        // UseCases
        services.AddScoped<IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>>, GetAllLevelUC>();
        services.AddScoped<IUseCase<GetByIdRequest, LevelDto>, GetLevelByIdUC>();
        services.AddScoped<IUseCase<CreateLevelRequest, LevelDto>, CreateLevelUC>();

        services.AddScoped<IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>>, GetAllCategoryUC>();
        services.AddScoped<IUseCase<GetByIdRequest, CategoryDto>, GetCategoryByIdUC>();

        // Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILevelRepository, LevelRepository>();

        return services;
    }
}
