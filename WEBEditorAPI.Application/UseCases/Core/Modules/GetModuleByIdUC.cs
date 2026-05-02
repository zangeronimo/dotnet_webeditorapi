using System;
using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Modules;

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
