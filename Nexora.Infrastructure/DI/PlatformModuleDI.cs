using Microsoft.Extensions.DependencyInjection;

using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Core;
using Nexora.Application.DTOs.System;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Application.Requests.UseCases.Core.Modules;
using Nexora.Application.Requests.UseCases.Core.Users;
using Nexora.Application.Requests.UseCases.System.ApiClients;
using Nexora.Application.Requests.UseCases.System.Roles;
using Nexora.Application.Requests.UseCases.System.UserCompanies;
using Nexora.Application.UseCases.Core;
using Nexora.Application.UseCases.Core.Companies;
using Nexora.Application.UseCases.Core.Modules;
using Nexora.Application.UseCases.Core.Users;
using Nexora.Application.UseCases.System.ApiClients;
using Nexora.Application.UseCases.System.Roles;
using Nexora.Application.UseCases.System.UserCompanies;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Interfaces.Repository.System;
using Nexora.Infrastructure.Repositories.Core;
using Nexora.Infrastructure.Repositories.System;

namespace Nexora.Infrastructure.DI;

public static class PlatformModuleDI
{
    public static IServiceCollection AddPlatformModule(this IServiceCollection services)
    {
        // UseCases
        services.AddScoped<IMakeLogin, MakeLoginUC>();
        services.AddScoped<IRefreshToken, RefreshTokenUC>();
        services.AddScoped<ISwitchCompany, SwitchCompanyUC>();

        services.AddScoped<IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>>, GetAllUsersUC>();
        services.AddScoped<IUseCase<GetByIdRequest, UserDto>, GetUserByIdUC>();
        services.AddScoped<IUseCase<GetUserProfileRequest, UserProfileDto>, GetUserProfileUC>();
        services.AddScoped<IUseCase<CreateUserRequest, UserDto>, CreateUserUC>();
        services.AddScoped<IUseCase<UpdateUserRequest, UserDto>, UpdateUserUC>();
        services.AddScoped<IUseCase<UserProfileRequest, UserProfileDto>, UserProfileUC>();
        services.AddScoped<IUseCase<UserProfileAvatarRequest, UserProfileAvatarDto>, UserProfileAvatarUC>();
        services.AddScoped<IUseCase<DeleteRequest, UserDto>, DeleteUserUC>();

        services.AddScoped<IUseCase<GetAllUserCompaniesFilterRequest, PaginationResult<UserCompanyDto>>, GetAllUserCompaniesUC>();
        services.AddScoped<IUseCase<GetByIdRequest, UserCompanyDto>, GetUserCompanyByIdUC>();
        services.AddScoped<IUseCase<GetAllCompanyApiClientsRequest, CompanyApiClientDto>, GetAllCompanyApiClientsUC>();
        services.AddScoped<IUseCase<GetUserCompanyModulesRequest, List<ModuleWithRolesDto>>, GetModulesWithRolesUC>();
        services.AddScoped<IUseCase<CreateUserCompanyRequest, UserCompanyDto>, CreateUserCompanyUC>();
        services.AddScoped<IUseCase<UpdateUserCompanyRequest, UserCompanyDto>, UpdateUserCompanyUC>();
        services.AddScoped<IUseCase<UpdateUserCompanyModulesRequest, UserCompanyDto>, UpdateUserCompanyModulesUC>();
        services.AddScoped<IUseCase<CompanyApiClientsRequest, CompanyApiClientDto>, CompanyApiClientsUC>();
        services.AddScoped<IUseCase<UpdateUserCompanyAvatarRequest, UserCompanyDto>, UpdateUserCompanyAvatarUC>();
        services.AddScoped<IUseCase<DeleteRequest, UserCompanyDto>, DeleteUserCompanyUC>();

        services.AddScoped<IUseCase<GetAllCompaniesFilterRequest, PaginationResult<CompanyDto>>, GetAllCompaniesUC>();
        services.AddScoped<IUseCase<GetAllCompanyModulesRequest, CompanyDto>, GetAllCompanyModulesUC>();
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

        services.AddScoped<IUseCase<GetAllApiClientsRequest, IEnumerable<ApiClientDto>>, GetAllApiClientsUC>();
        services.AddScoped<IUseCase<GenerateApiClientRequest, GenerateApiClientSecretDto>, GenerateApiClientUC>();

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCompanyRepository, UserCompanyRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IApiClientRepository, ApiClientRepository>();

        return services;
    }
}
