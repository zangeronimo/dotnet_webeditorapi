using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Roles;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Roles;

public class CreateRoleUC(IRoleRepository roleRepository, IMapper mapper) : IUseCase<CreateRoleRequest, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RoleDto> ExecuteAsync(CreateRoleRequest request)
    {
        Role? role = await _roleRepository.GetByNameAsync(request.Name, request.Context.CompanyId);
        if (role != null)
            throw new ApiBadRequestException(RoleErrors.AlreadyExists);
        Role newRole = new Role(request.Name, request.Status, request.Context.CompanyId);
        await _roleRepository.AddAsync(newRole);
        Role? createdRole = await _roleRepository.GetByIdAsync(newRole.Id, request.Context.CompanyId);
        return _mapper.Map<RoleDto>(createdRole);
    }
}
