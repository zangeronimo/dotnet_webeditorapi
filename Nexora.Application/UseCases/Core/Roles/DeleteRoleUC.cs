using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Roles;

public class DeleteRoleUC(IRoleRepository roleRepository, IMapper mapper) : IUseCase<DeleteRequest, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RoleDto> ExecuteAsync(DeleteRequest request)
    {
        Role? role = await _roleRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (role == null)
            throw new ApiNotFoundException(RoleErrors.NotFound);
        role.Delete();
        await _roleRepository.UpdateAsync(role);
        return _mapper.Map<RoleDto>(role);
    }
}
