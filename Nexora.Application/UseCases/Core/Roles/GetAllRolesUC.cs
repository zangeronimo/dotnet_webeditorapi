using AutoMapper;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Roles;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Roles;

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
