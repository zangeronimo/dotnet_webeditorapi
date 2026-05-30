using AutoMapper;
using Nexora.Application.DTOs.System;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.System.Roles;
using Nexora.Domain.Entities.System;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.Roles;

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
