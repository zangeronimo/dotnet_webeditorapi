using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Levels;

public class GetAllLevelUC(ILevelRepository levelRepository, IMapper mapper) : IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>>
{
    private readonly ILevelRepository _levelRepository = levelRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<LevelDto>> ExecuteAsync(GetAllLevelsFilterRequest request)
    {
        (IEnumerable<Level> levels, int total) = await _levelRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Active, request.Context.CompanyId);

        return new PaginationResult<LevelDto>
        {
            Items = _mapper.Map<IEnumerable<LevelDto>>(levels),
            Total = total
        };
    }
}
