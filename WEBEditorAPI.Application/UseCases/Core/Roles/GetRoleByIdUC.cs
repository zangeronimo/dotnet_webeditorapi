using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Roles;

public class GetRoleByIdUC(IRoleRepository roleRepository, IMapper mapper) : IUseCase<GetByIdRequest, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<RoleDto> ExecuteAsync(GetByIdRequest request)
    {
        Role? role = await _roleRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (role == null)
            throw new ApiNotFoundException(RoleErrors.NotFound);
        return _mapper.Map<RoleDto>(role);
    }
}