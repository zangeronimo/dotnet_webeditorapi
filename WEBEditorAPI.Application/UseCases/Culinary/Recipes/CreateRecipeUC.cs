using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Domain.ValueObjects;
using WEBEditorAPI.Domain.ValueObjects.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Recipes;

public class CreateRecipeUC(IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<CreateRecipeRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RecipeDto> ExecuteAsync(CreateRecipeRequest request)
    {
        var slug = Slug.Create(request.Name);
        Recipe? recipe = await _recipeRepository.GetBySlugAsync(slug.Value, request.Context.CompanyId);
        if (recipe != null)
            throw new ApiBadRequestException("Recipe já cadastrada com esse slug");
        Recipe newRecipe = new Recipe(
            slug,
            request.Name,
            request.Content,
            request.Timing,
            request.Yield,
            request.Attributes,
            new RecipeMedia(""),
            request.Seo,
            new RecipeEngagement(0, 0),
            Status.Inactive,
            request.LevelId,
            request.Context.CompanyId);
        await _recipeRepository.AddAsync(newRecipe);
        Recipe? createdRecipe = await _recipeRepository.GetByIdAsync(newRecipe.Id, newRecipe.CompanyId);
        return _mapper.Map<RecipeDto>(createdRecipe);
    }
}
