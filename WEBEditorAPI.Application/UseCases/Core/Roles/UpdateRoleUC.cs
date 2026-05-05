using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Roles;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Roles;

public class UpdateRoleUC(IRoleRepository roleRepository, IMapper mapper) : IUseCase<UpdateRoleRequest, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RoleDto> ExecuteAsync(UpdateRoleRequest request)
    {
        Role? role = await _roleRepository.GetByNameAsync(request.Name, request.Context.CompanyId);
        if (role != null && role.Id != request.Id)
            throw new ApiBadRequestException(RoleErrors.AlreadyExists);
        Role? updateRole = await _roleRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateRole == null)
            throw new ApiBadRequestException(RoleErrors.NotFound);
        updateRole.Update(request.Name, request.Active);
        await _roleRepository.UpdateAsync(updateRole);
        Role? updatedRole = await _roleRepository.GetByIdAsync(updateRole.Id, request.Context.CompanyId);
        return _mapper.Map<RoleDto>(updatedRole);
    }
}
