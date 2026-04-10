using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Domain.Commands.Culinary;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Application.UseCases.Culinary.Levels;

public class UpdateLevelUC(ILevelRepository levelRepository, IMapper mapper) : IUseCase<UpdateLevelRequest, LevelDto>
{
    private readonly ILevelRepository _levelRepository = levelRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<LevelDto> ExecuteAsync(UpdateLevelRequest request)
    {
        var slug = Slug.Restore(request.Slug);
        Level? level = await _levelRepository.GetBySlugAsync(slug.Value, request.Context.CompanyId);
        if (level != null && level.Id != request.Id)
            throw new ApiBadRequestException("Level já cadastrado com esse slug");
        Level? updateLevel = await _levelRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateLevel == null)
            throw new ApiBadRequestException("Level não encontrado.");
        updateLevel.Update(slug, request.Name, request.Active);
        var commands = CreateCategoryCommand(request.CategoriesDtos);
        updateLevel.UpdateCategories(commands);
        await _levelRepository.UpdateAsync(updateLevel);
        Level? updatedLevel = await _levelRepository.GetByIdAsync(updateLevel.Id, updateLevel.CompanyId);
        return _mapper.Map<LevelDto>(updatedLevel);
    }

    private IEnumerable<UpdateCategoryCommand> CreateCategoryCommand(List<CategoryDto> categoriesDtos)
    {
        return categoriesDtos.Select(dto => new UpdateCategoryCommand(
            dto.Id,
            dto.Id != Guid.Empty ? Slug.Restore(dto.Slug) : Slug.Create(dto.Name),
            dto.Name,
            dto.Active
        ));
    }
}
