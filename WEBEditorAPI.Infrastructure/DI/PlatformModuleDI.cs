using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Core.Modules;
using WEBEditorAPI.Application.UseCases.Core;
using WEBEditorAPI.Application.UseCases.Core.Modules;
using WEBEditorAPI.Application.UseCases.System.Users;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Infrastructure.Repositories.Core;
using WEBEditorAPI.Infrastructure.Repositories.System;

namespace WEBEditorAPI.Infrastructure.DI;

public static class PlatformModuleDI
{
    public static IServiceCollection AddPlatformModule(this IServiceCollection services)
    {
        // UseCases
        services.AddScoped<IMakeLogin, MakeLoginUC>();
        services.AddScoped<IRefreshToken, RefreshTokenUC>();

        // services.AddScoped<IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>>, GetAllUsersUC>();
        services.AddScoped<IUseCase<GetByIdRequest, UserDto>, GetUserByIdUC>();
        // services.AddScoped<IUseCase<CreateUserRequest, UserDto>, CreateUserUC>();
        // services.AddScoped<IUseCase<UpdateUserRequest, UserDto>, UpdateUserUC>();
        // services.AddScoped<IUseCase<DeleteRequest, UserDto>, DeleteUserUC>();

        services.AddScoped<IUseCase<GetAllModulesFilterRequest, PaginationResult<ModuleDto>>, GetAllModulesUC>();
        services.AddScoped<IUseCase<GetByIdRequest, ModuleDto>, GetModuleByIdUC>();
        services.AddScoped<IUseCase<CreateModuleRequest, ModuleDto>, CreateModuleUC>();
        services.AddScoped<IUseCase<UpdateModuleRequest, ModuleDto>, UpdateModuleUC>();
        services.AddScoped<IUseCase<DeleteRequest, ModuleDto>, DeleteModuleUC>();

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCompanyRepository, UserCompanyRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        return services;
    }
}
