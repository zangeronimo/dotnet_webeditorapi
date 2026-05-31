using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Modules;
using Nexora.Domain.Commands.Core;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Exceptions;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Modules;

public class UpdateModuleUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<UpdateModuleRequest, ModuleDto>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<ModuleDto> ExecuteAsync(UpdateModuleRequest request)
    {
        Module? module = await _moduleRepository.GetByNameAsync(request.Name);
        if (module != null && module.Id != request.Id)
            throw new ApiBadRequestException(ModuleErrors.AlreadyExists);
        Module? updateModule = await _moduleRepository.GetByIdAsync(request.Id);
        if (updateModule == null)
            throw new ApiNotFoundException(ModuleErrors.NotFound);
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
