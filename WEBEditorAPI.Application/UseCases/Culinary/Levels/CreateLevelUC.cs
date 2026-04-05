using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Application.UseCases.Culinary.Levels;

public class CreateLevelUC(ILevelRepository levelRepository, IMapper mapper) : IUseCase<CreateLevelRequest, LevelDto>
{
    private readonly ILevelRepository _levelRepository = levelRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<LevelDto> ExecuteAsync(CreateLevelRequest request)
    {
        var slug = Slug.Create(request.Name);
        Level? level = await _levelRepository.GetBySlugAsync(slug.Value, request.Context.CompanyId);
        if (level != null)
            throw new ApiBadRequestException("Level já cadastrado com esse slug");
        Level newLevel = new Level(slug, request.Name, request.Active, request.Context.CompanyId);
        await _levelRepository.AddAsync(newLevel);
        Level? createdLevel = await _levelRepository.GetByIdAsync(newLevel.Id, newLevel.CompanyId);
        return _mapper.Map<LevelDto>(createdLevel);
    }
}