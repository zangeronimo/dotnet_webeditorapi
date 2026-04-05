using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Levels;

public class GetLevelByIdUC(ILevelRepository levelRepository, IMapper mapper) : IUseCase<GetByIdRequest, LevelDto>
{
    private readonly ILevelRepository _levelRepository = levelRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<LevelDto> ExecuteAsync(GetByIdRequest request)
    {
        Level? level = await _levelRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (level == null)
            throw new ApiNotFoundException("Level não encontrado");
        return _mapper.Map<LevelDto>(level);
    }
}
