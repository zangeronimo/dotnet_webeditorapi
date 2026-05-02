using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Application.Exceptions;

namespace WEBEditorAPI.Application.UseCases.Core.Modules;

public class DeleteModuleUC(IModuleRepository ModuleRepository, IMapper mapper) : IUseCase<DeleteRequest, ModuleDto>
{
    private readonly IModuleRepository _ModuleRepository = ModuleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ModuleDto> ExecuteAsync(DeleteRequest request)
    {
        Module? Module = await _ModuleRepository.GetByIdAsync(request.ResourceId);
        if (Module == null)
            throw new ApiNotFoundException("Módulo não encontrado");
        Module.Delete();
        await _ModuleRepository.UpdateAsync(Module);
        return _mapper.Map<ModuleDto>(Module);
    }
}
