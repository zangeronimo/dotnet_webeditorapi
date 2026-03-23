using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Models.System;
using WEBEditorAPI.Application.UseCases.System;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Infrastructure.Options;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Provider;
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

        // Provider
        services.AddSingleton<IPasswordProvider, PBKDF2PasswordProvider>();
        services.AddSingleton<ITokenProvider, JwtProvider>();

        // UseCases
        services.AddScoped<IUseCase<AuthRequest, AuthResponse>, MakeLoginUC>();
        services.AddScoped<IUseCase<string, AuthResponse>, RefreshTokenUC>();
        services.AddScoped<IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>>, GetAllUsersUC>();
        services.AddScoped<IUseCase<GetUserByIdRequest, UserDto>, GetUserByIdUC>();

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();

        return services;
    }
}
