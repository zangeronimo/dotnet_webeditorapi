using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.UserCompanies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.UserCompanies;

public class UpdateUserCompanyModulesUC(
    IUserCompanyRepository userCompanyRepository,
    IModuleRepository moduleRepository,
    IRoleRepository roleRepository,
    IMapper mapper)
    : IUseCase<UpdateUserCompanyModulesRequest, UserCompanyDto>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserCompanyDto> ExecuteAsync(UpdateUserCompanyModulesRequest request)
    {
        UserCompany userCompany = await GetUserCompanyAsync(request);

        ValidateDuplicatedModules(request);

        List<Guid> moduleIds = request.Roles
            .Select(x => x.ModuleId)
            .Distinct()
            .ToList();

        List<Guid> roleIds = request.Roles
            .Select(x => x.RoleId)
            .Distinct()
            .ToList();

        List<Module> modules = await GetAndValidateModulesAsync(
            moduleIds,
            request.Context.CompanyId);

        List<Role> roles = await GetAndValidateRolesAsync(roleIds);

        ValidateRoleModules(request, roles);

        userCompany.SetModuleRoles(
            request.Roles.Select(r => (r.ModuleId, r.RoleId)));

        await _userCompanyRepository.UpdateAsync(userCompany);

        UserCompany updatedUserCompany = await GetUpdatedUserCompanyAsync(
            userCompany.Id,
            request.Context.CompanyId);

        return _mapper.Map<UserCompanyDto>(updatedUserCompany);
    }

    private async Task<UserCompany> GetUserCompanyAsync(
        UpdateUserCompanyModulesRequest request)
    {
        UserCompany? userCompany = await _userCompanyRepository
            .GetByIdAsync(request.UserCompanyId, request.Context.CompanyId);

        if (userCompany == null)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);

        return userCompany;
    }

    private static void ValidateDuplicatedModules(
        UpdateUserCompanyModulesRequest request)
    {
        bool hasDuplicatedModules = request.Roles
            .GroupBy(x => x.ModuleId)
            .Any(g => g.Count() > 1);

        if (hasDuplicatedModules)
            throw new ApiBadRequestException(ModuleErrors.Invalid);
    }

    private async Task<List<Module>> GetAndValidateModulesAsync(
        List<Guid> moduleIds,
        Guid companyId)
    {
        List<Module> modules = await _moduleRepository
            .GetByRangeIdAsync(moduleIds);

        if (modules.Count != moduleIds.Count)
            throw new ApiNotFoundException(ModuleErrors.NotFound);

        bool hasInvalidCompanyModule = modules.Any(m =>
            m.CompanyModules.All(cm =>
                cm.CompanyId != companyId));

        if (hasInvalidCompanyModule)
            throw new ApiNotFoundException(ModuleErrors.NotFound);

        return modules;
    }

    private async Task<List<Role>> GetAndValidateRolesAsync(
        List<Guid> roleIds)
    {
        List<Role> roles = await _roleRepository
            .GetByRangeIdAsync(roleIds);

        if (roles.Count != roleIds.Count)
            throw new ApiNotFoundException(RoleErrors.NotFound);

        return roles;
    }

    private static void ValidateRoleModules(
        UpdateUserCompanyModulesRequest request,
        List<Role> roles)
    {
        foreach (var moduleRole in request.Roles)
        {
            Role role = roles.First(r => r.Id == moduleRole.RoleId);

            bool roleBelongsToModule = role.RolePermissions
                .Any(rp => rp.Permission.ModuleId == moduleRole.ModuleId);

            if (!roleBelongsToModule)
                throw new ApiBadRequestException(RoleErrors.Invalid);
        }
    }

    private async Task<UserCompany> GetUpdatedUserCompanyAsync(
        Guid userCompanyId,
        Guid companyId)
    {
        UserCompany? updatedUserCompany = await _userCompanyRepository
            .GetByIdAsync(userCompanyId, companyId);

        if (updatedUserCompany == null)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);

        return updatedUserCompany;
    }
}