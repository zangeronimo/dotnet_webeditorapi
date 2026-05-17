using System;
using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Modules;

public class GetModuleByIdUC(IModuleRepository moduleRepository, IMapper mapper) : IUseCase<GetByIdRequest, ModuleDto>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<ModuleDto> ExecuteAsync(GetByIdRequest request)
    {
        Module? module = await _moduleRepository.GetByIdAsync(request.ResourceId);
        if (module == null)
            throw new ApiNotFoundException("Módulo não encontrado");
        return _mapper.Map<ModuleDto>(module);
    }
}
