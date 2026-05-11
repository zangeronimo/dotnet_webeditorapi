using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Core.Companies;
using WEBEditorAPI.Application.Requests.UseCases.Core.Modules;
using WEBEditorAPI.Application.Requests.UseCases.Core.Roles;
using WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;
using WEBEditorAPI.Application.Requests.UseCases.Core.Users;
using WEBEditorAPI.Application.UseCases.Core;
using WEBEditorAPI.Application.UseCases.Core.Companies;
using WEBEditorAPI.Application.UseCases.Core.Modules;
using WEBEditorAPI.Application.UseCases.Core.Roles;
using WEBEditorAPI.Application.UseCases.Core.UserCompanies;
using WEBEditorAPI.Application.UseCases.Core.Users;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Infrastructure.Repositories.Core;

namespace WEBEditorAPI.Infrastructure.DI;

public static class PlatformModuleDI
{
    public static IServiceCollection AddPlatformModule(this IServiceCollection services)
    {
        // UseCases
        services.AddScoped<IMakeLogin, MakeLoginUC>();
        services.AddScoped<IRefreshToken, RefreshTokenUC>();

        services.AddScoped<IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>>, GetAllUsersUC>();
        services.AddScoped<IUseCase<GetByIdRequest, UserDto>, GetUserByIdUC>();
        services.AddScoped<IUseCase<CreateUserRequest, UserDto>, CreateUserUC>();
        services.AddScoped<IUseCase<UpdateUserRequest, UserDto>, UpdateUserUC>();
        services.AddScoped<IUseCase<UserProfileRequest, UserProfileDto>, UserProfileUC>();
        services.AddScoped<IUseCase<UserProfileAvatarRequest, UserProfileAvatarDto>, UserProfileAvatarUC>();
        services.AddScoped<IUseCase<DeleteRequest, UserDto>, DeleteUserUC>();

        services.AddScoped<IUseCase<GetAllUserCompaniesFilterRequest, PaginationResult<UserCompanyDto>>, GetAllUserCompaniesUC>();
        services.AddScoped<IUseCase<GetByIdRequest, UserCompanyDto>, GetUserCompanyByIdUC>();
        services.AddScoped<IUseCase<GetUserCompanyModulesRequest, List<ModuleWithRolesDto>>, GetModulesWithRolesUC>();
        services.AddScoped<IUseCase<CreateUserCompanyRequest, UserCompanyDto>, CreateUserCompanyUC>();
        services.AddScoped<IUseCase<UpdateUserCompanyRequest, UserCompanyDto>, UpdateUserCompanyUC>();
        services.AddScoped<IUseCase<UpdateUserCompanyModulesRequest, UserCompanyDto>, UpdateUserCompanyModulesUC>();
        services.AddScoped<IUseCase<UpdateUserCompanyAvatarRequest, UserCompanyDto>, UpdateUserCompanyAvatarUC>();
        services.AddScoped<IUseCase<DeleteRequest, UserCompanyDto>, DeleteUserCompanyUC>();

        services.AddScoped<IUseCase<GetAllCompaniesFilterRequest, PaginationResult<CompanyDto>>, GetAllCompaniesUC>();
        services.AddScoped<IUseCase<GetByIdRequest, CompanyDto>, GetCompanyByIdUC>();
        services.AddScoped<IUseCase<CreateCompanyRequest, CompanyDto>, CreateCompanyUC>();
        services.AddScoped<IUseCase<UpdateModulesRequest, CompanyDto>, UpdateModulesUC>();
        services.AddScoped<IUseCase<UpdateCompanyRequest, CompanyDto>, UpdateCompanyUC>();
        services.AddScoped<IUseCase<DeleteRequest, CompanyDto>, DeleteCompanyUC>();
        services.AddScoped<IUseCase<CompanyProfileRequest, CompanyDto>, CompanyProfileUC>();

        services.AddScoped<IUseCase<GetAllModulesFilterRequest, PaginationResult<ModuleDto>>, GetAllModulesUC>();
        services.AddScoped<IUseCase<GetByIdRequest, ModuleDto>, GetModuleByIdUC>();
        services.AddScoped<IUseCase<CreateModuleRequest, ModuleDto>, CreateModuleUC>();
        services.AddScoped<IUseCase<UpdateModuleRequest, ModuleDto>, UpdateModuleUC>();
        services.AddScoped<IUseCase<DeleteRequest, ModuleDto>, DeleteModuleUC>();

        services.AddScoped<IUseCase<GetAllRolesFilterRequest, PaginationResult<RoleDto>>, GetAllRolesUC>();
        services.AddScoped<IUseCase<GetByIdRequest, RoleDto>, GetRoleByIdUC>();
        services.AddScoped<IUseCase<CreateRoleRequest, RoleDto>, CreateRoleUC>();
        services.AddScoped<IUseCase<UpdateRoleRequest, RoleDto>, UpdateRoleUC>();
        services.AddScoped<IUseCase<UpdatePermissionsRequest, RoleDto>, UpdatePermissionsUC>();
        services.AddScoped<IUseCase<DeleteRequest, RoleDto>, DeleteRoleUC>();


        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCompanyRepository, UserCompanyRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}
