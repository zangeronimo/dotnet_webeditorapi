using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.UseCases.System;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Provider;
using WEBEditorAPI.Infrastructure.Repositories.System;

namespace WEBEditorAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        // Provider
        services.AddSingleton<IPasswordProvider, PBKDF2PasswordProvider>();

        // UseCases
        services.AddScoped<IUseCase<AuthRequest, AuthResponse>, MakeLoginUC>();

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();

        return services;
    }
}
