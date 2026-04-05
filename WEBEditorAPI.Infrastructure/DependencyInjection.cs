using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Categories;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Application.Requests.UseCases.System.Users;
using WEBEditorAPI.Application.UseCases.Culinary.Categories;
using WEBEditorAPI.Application.UseCases.Culinary.Levels;
using WEBEditorAPI.Application.UseCases.System;
using WEBEditorAPI.Application.UseCases.System.Users;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Infrastructure.Options;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Provider;
using WEBEditorAPI.Infrastructure.Repositories.Culinary;
using WEBEditorAPI.Infrastructure.Repositories.System;

namespace WEBEditorAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // DbContext
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseNpgsql(dbOptions.ConnectionString);
        });
        services.AddDbContext<CulinaryDbContext>((sp, options) =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseNpgsql(dbOptions.ConnectionString);
        });

        // Provider
        services.AddSingleton<IPasswordProvider, PBKDF2PasswordProvider>();
        services.AddSingleton<ITokenProvider, JwtProvider>();

        // UseCases
        services.AddScoped<IMakeLogin, MakeLoginUC>();
        services.AddScoped<IRefreshToken, RefreshTokenUC>();
        services.AddScoped<IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>>, GetAllUsersUC>();
        services.AddScoped<IUseCase<GetUserByIdRequest, UserDto>, GetUserByIdUC>();
        services.AddScoped<IUseCase<CreateUserRequest, UserDto>, CreateUserUC>();
        services.AddScoped<IUseCase<UpdateUserRequest, UserDto>, UpdateUserUC>();
        services.AddScoped<IUseCase<DeleteUserRequest, UserDto>, DeleteUserUC>();

        services.AddScoped<IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>>, GetAllCategoryUC>();
        services.AddScoped<IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>>, GetAllLevelUC>();

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILevelRepository, LevelRepository>();

        return services;
    }
}
