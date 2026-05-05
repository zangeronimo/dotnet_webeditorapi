using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Roles;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Roles;

public class UpdatePermissionsUC(IRoleRepository roleRepository, IPermissionRepository permissionRepository, IMapper mapper) : IUseCase<UpdatePermissionsRequest, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RoleDto> ExecuteAsync(UpdatePermissionsRequest request)
    {
        Role? role = await _roleRepository.GetByIdAsync(request.RoleId, request.Context.CompanyId);
        if (role == null)
            throw new ApiBadRequestException(RoleErrors.NotFound);
        var permissions = await _permissionRepository.GetByRangeIdAsync(request.PermissionIds, request.Context.CompanyId);
        if (permissions.Count != request.PermissionIds.Count)
        {
            throw new ApiBadRequestException(PermissionError.SomePermissionWasNotExists);
        }
        role.SetPermissions(permissions);
        await _roleRepository.UpdateAsync(role);
        var updatedCompany = await _roleRepository.GetByIdReadOnlyAsync(role.Id, role.CompanyId);
        return _mapper.Map<RoleDto>(updatedCompany);
    }
}
