using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Application.UseCases.Culinary.Recipes;

public class UpdateRecipeUC(IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<UpdateRecipeRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RecipeDto> ExecuteAsync(UpdateRecipeRequest request)
    {
        var slug = Slug.Restore(request.Slug);
        Recipe? recipe = await _recipeRepository.GetBySlugAsync(slug.Value, request.Context.CompanyId);
        if (recipe != null && recipe.Id != request.Id)
            throw new ApiBadRequestException("Receita já cadastrada com esse slug");
        Recipe? updateRecipe = await _recipeRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateRecipe == null)
            throw new ApiBadRequestException("Receita não encontrada");
        updateRecipe.Update(slug, request.Name, request.Content, request.Timing, request.Yield, request.Attributes, null, request.Seo, request.Active, request.LevelId);
        await _recipeRepository.UpdateAsync(updateRecipe);
        Recipe? updatedRecipe = await _recipeRepository.GetByIdAsync(updateRecipe.Id, updateRecipe.CompanyId);
        return _mapper.Map<RecipeDto>(updatedRecipe);
    }
}
