using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Entities.Core;
using Nexora.Application.Exceptions;
using Nexora.Domain.Errors.Core;

namespace Nexora.Application.UseCases.Core.Modules;

public class DeleteModuleUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<DeleteRequest, ModuleDto>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ModuleDto> ExecuteAsync(DeleteRequest request)
    {
        Module? module = await _moduleRepository.GetByIdAsync(request.ResourceId);
        if (module == null)
            throw new ApiNotFoundException(ModuleErrors.NotFound);
        module.Delete();
        await _moduleRepository.UpdateAsync(module);
        return _mapper.Map<ModuleDto>(module);
    }
}
