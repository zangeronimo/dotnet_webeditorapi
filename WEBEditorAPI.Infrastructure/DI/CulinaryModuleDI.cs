using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Application.UseCases.Culinary.Levels;
using WEBEditorAPI.Application.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Repositories.Culinary;

namespace WEBEditorAPI.Infrastructure.DI;

public static class CulinaryModuleDI
{
    public static IServiceCollection AddCulinaryModule(this IServiceCollection services)
    {
        // UseCases
        services.AddScoped<IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>>, GetAllLevelUC>();
        services.AddScoped<IUseCase<GetByIdRequest, LevelDto>, GetLevelByIdUC>();
        services.AddScoped<IUseCase<CreateLevelRequest, LevelDto>, CreateLevelUC>();
        services.AddScoped<IUseCase<UpdateLevelRequest, LevelDto>, UpdateLevelUC>();

        services.AddScoped<IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>>, GetAllRecipeUC>();
        services.AddScoped<IUseCase<GetByIdRequest, RecipeDto>, GetRecipeByIdUC>();
        services.AddScoped<IUseCase<CreateRecipeRequest, RecipeDto>, CreateRecipeUC>();
        services.AddScoped<IUseCase<UpdateRecipeRequest, RecipeDto>, UpdateRecipeUC>();

        // Repositories
        services.AddScoped<ILevelRepository, LevelRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();

        return services;
    }
}
