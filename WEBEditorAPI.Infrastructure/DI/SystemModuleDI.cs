using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.System.Users;
using WEBEditorAPI.Application.UseCases.System;
using WEBEditorAPI.Application.UseCases.System.Users;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Infrastructure.Repositories.System;

namespace WEBEditorAPI.Infrastructure.DI;

public static class SystemModuleDI
{
    public static IServiceCollection AddSystemModule(this IServiceCollection services)
    {
        // UseCases
        services.AddScoped<IMakeLogin, MakeLoginUC>();
        services.AddScoped<IRefreshToken, RefreshTokenUC>();

        services.AddScoped<IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>>, GetAllUsersUC>();
        services.AddScoped<IUseCase<GetUserByIdRequest, UserDto>, GetUserByIdUC>();
        services.AddScoped<IUseCase<CreateUserRequest, UserDto>, CreateUserUC>();
        services.AddScoped<IUseCase<UpdateUserRequest, UserDto>, UpdateUserUC>();
        services.AddScoped<IUseCase<DeleteUserRequest, UserDto>, DeleteUserUC>();

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();

        return services;
    }
}
