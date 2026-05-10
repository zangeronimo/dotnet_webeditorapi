using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.UserCompanies;

public class GetModulesWithRolesUC(
        IModuleRepository moduleRepository,
        IRoleRepository roleRepository,
        IUserCompanyRepository userCompanyRepository) : IUseCase<GetUserCompanyModulesRequest, List<ModuleWithRolesDto>>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;

    public async Task<List<ModuleWithRolesDto>> ExecuteAsync(GetUserCompanyModulesRequest request)
    {
        List<ModuleWithRolesDto> modulesWithRolesDto = new List<ModuleWithRolesDto>();
        List<Module> modules = await _moduleRepository.GetAllByCompanyIdAsync(request.Context.CompanyId);
        List<Role> roles = await _roleRepository.GetAllByCompanyIdAsync(request.Context.CompanyId);
        List<UserCompanyModuleRole> selectedRoles = await _userCompanyRepository.GetUserCompanyModuleRoleAsync(request.ResourceId);

        foreach (var module in modules)
        {
            List<Role> moduleRoles = roles
                .Where(r => r.RolePermissions
                    .Any(rp => rp.Permission.ModuleId == module.Id))
                .ToList();

            UserCompanyModuleRole? selectedRole = selectedRoles
                .FirstOrDefault(x => x.ModuleId == module.Id);

            modulesWithRolesDto.Add(new ModuleWithRolesDto
            {
                Module = new ModuleAccessDto() { Id = module.Id, Name = module.Name },
                SelectedRoleId = selectedRole?.RoleId,
                Roles = moduleRoles.Select(r => new RoleAccessDto
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            });
        }

        return modulesWithRolesDto;
    }
}