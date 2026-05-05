using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Roles;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Roles;

public class GetAllRolesUC(IRoleRepository roleRepository, IMapper mapper) : IUseCase<GetAllRolesFilterRequest, PaginationResult<RoleDto
>>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<RoleDto>> ExecuteAsync(GetAllRolesFilterRequest request)
    {
        (IEnumerable<Role> roles, int total) = await _roleRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Status, request.Context.CompanyId);

        return new PaginationResult<RoleDto>
        {
            Items = _mapper.Map<IEnumerable<RoleDto>>(roles),
            Total = total
        };
    }
}
