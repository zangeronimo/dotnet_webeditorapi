using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Application.Exceptions;

namespace WEBEditorAPI.Application.UseCases.Core.Modules;

public class DeleteModuleUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<DeleteRequest, ModuleDto>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ModuleDto> ExecuteAsync(DeleteRequest request)
    {
        Module? module = await _moduleRepository.GetByIdAsync(request.ResourceId);
        if (module == null)
            throw new ApiNotFoundException("Módulo não encontrado");
        module.Delete();
        await _moduleRepository.UpdateAsync(module);
        return _mapper.Map<ModuleDto>(module);
    }
}
