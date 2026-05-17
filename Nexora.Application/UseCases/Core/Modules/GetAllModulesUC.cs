using AutoMapper;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Modules;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Modules;

public class GetAllModulesUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<GetAllModulesFilterRequest, PaginationResult<ModuleDto>>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<ModuleDto>> ExecuteAsync(GetAllModulesFilterRequest request)
    {
        (IEnumerable<Module> modules, int total) = await _moduleRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Status);

        return new PaginationResult<ModuleDto>
        {
            Items = _mapper.Map<IEnumerable<ModuleDto>>(modules),
            Total = total
        };
    }
}
