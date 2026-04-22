using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Domain.ValueObjects;
using WEBEditorAPI.Domain.ValueObjects.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Recipes;

public class UpdateRecipeUC(IRecipeRepository recipeRepository, IStorageProvider storageProvider, IMapper mapper) : IUseCase<UpdateRecipeRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IStorageProvider _storageProvider = storageProvider;
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
        var media = new RecipeMedia("");
        if (!string.IsNullOrEmpty(request.Image))
        {
            if (!string.IsNullOrEmpty(updateRecipe.Media.ImageUrl))
                await _storageProvider.DeleteFileAsync(updateRecipe.Media.ImageUrl);
            var imageUrl = await _storageProvider.SaveFileAsync(request.Image, request.Context.CompanyId.ToString());
            media = new RecipeMedia(imageUrl);
        }
        updateRecipe.Update(slug, request.Name, request.Content, request.Timing, request.Yield, request.Attributes, media, request.Seo, request.Active, request.LevelId);
        await _recipeRepository.UpdateAsync(updateRecipe);
        Recipe? updatedRecipe = await _recipeRepository.GetByIdAsync(updateRecipe.Id, updateRecipe.CompanyId);
        return _mapper.Map<RecipeDto>(updatedRecipe);
    }
}
