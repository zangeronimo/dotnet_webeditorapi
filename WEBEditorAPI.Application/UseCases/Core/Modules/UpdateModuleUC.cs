using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Modules;
using WEBEditorAPI.Domain.Commands.Core;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Exceptions;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Modules;

public class UpdateModuleUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<UpdateModuleRequest, ModuleDto>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<ModuleDto> ExecuteAsync(UpdateModuleRequest request)
    {
        Module? module = await _moduleRepository.GetByNameAsync(request.Name);
        if (module != null && module.Id != request.Id)
            throw new ApiBadRequestException("Módulo já cadastrado com esse nome");
        Module? updateModule = await _moduleRepository.GetByIdAsync(request.Id);
        if (updateModule == null)
            throw new ApiBadRequestException("Módulo não encontrado.");
        updateModule.Update(request.Name, request.Active);
        var commands = CreatePermissionCommand(request.PermissionsDtos);
        try
        {
            updateModule.UpdatePermissions(commands);
            await _moduleRepository.UpdateAsync(updateModule);
        }
        catch (DomainException ex)
        {
            throw new ApiBadRequestException(ex.Message);
        }
        Module? updatedModule = await _moduleRepository.GetByIdReadOnlyAsync(updateModule.Id);
        return _mapper.Map<ModuleDto>(updatedModule);
    }

    private IEnumerable<UpdatePermissionCommand> CreatePermissionCommand(List<PermissionDto> categoriesDtos)
    {
        return categoriesDtos.Select(dto => new UpdatePermissionCommand(
            dto.Id,
            dto.Code,
            dto.Label,
            dto.Status
        ));
    }
}
