using AutoMapper;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.System;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.System.Roles;
using Nexora.Domain.Entities.System;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.Roles;

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
