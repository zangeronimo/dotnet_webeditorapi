using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Modules;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Modules;

public class CreateModuleUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<CreateModuleRequest, ModuleDto>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<ModuleDto> ExecuteAsync(CreateModuleRequest request)
    {
        Module? module = await _moduleRepository.GetByNameAsync(request.Name);
        if (module != null)
            throw new ApiBadRequestException(ModuleErrors.AlreadyExists);
        Module newModule = new Module(request.Name, request.Status);
        await _moduleRepository.AddAsync(newModule);
        Module? createdModule = await _moduleRepository.GetByIdAsync(newModule.Id);
        return _mapper.Map<ModuleDto>(createdModule);
    }
}
