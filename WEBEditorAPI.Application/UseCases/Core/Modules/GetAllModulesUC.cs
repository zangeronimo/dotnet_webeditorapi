using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Modules;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Modules;

public class GetAllModulesUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<GetAllModulesFilterRequest, PaginationResult<ModuleDto>>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<ModuleDto>> ExecuteAsync(GetAllModulesFilterRequest request)
    {
        (IEnumerable<Module> modules, int total) = await _moduleRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Active);

        return new PaginationResult<ModuleDto>
        {
            Items = _mapper.Map<IEnumerable<ModuleDto>>(modules),
            Total = total
        };
    }
}
