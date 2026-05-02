using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Modules;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Modules;

public class CreateModuleUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<CreateModuleRequest, ModuleDto>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<ModuleDto> ExecuteAsync(CreateModuleRequest request)
    {
        Module? module = await _moduleRepository.GetByNameAsync(request.Name);
        if (module != null)
            throw new ApiBadRequestException("Módule já cadastrado com esse nome");
        Module newModule = new Module(request.Name, request.Status);
        await _moduleRepository.AddAsync(newModule);
        Module? createdModule = await _moduleRepository.GetByIdAsync(newModule.Id);
        return _mapper.Map<ModuleDto>(createdModule);
    }
}
