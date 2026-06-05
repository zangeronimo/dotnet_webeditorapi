using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Recipes;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culiarny;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Application.UseCases.Culinary.Recipes;

public class RecipeImageUploadUC(IRecipeRepository recipeRepository, IStorageProvider storageProvider, IMapper mapper) : IUseCase<RecipeImageUploadRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IStorageProvider _storageProvider = storageProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<RecipeDto> ExecuteAsync(RecipeImageUploadRequest request)
    {
        Recipe? recipe = await _recipeRepository.GetByIdAsync(request.RecipeId, request.Context.CompanyId);
        if (recipe == null)
            throw new ApiNotFoundException(RecipeErrors.NotFound);
        var imageUrl = await _storageProvider.SaveStreamAsync(request.Image, request.Context.CompanyId.ToString(), "culinary/recipe");
        var media = new RecipeMedia(imageUrl);
        recipe.SetMedia(media);
        await _recipeRepository.UpdateAsync(recipe);
        Recipe? updatedRecipe = await _recipeRepository.GetByIdAsync(recipe.Id, request.Context.CompanyId);
        return _mapper.Map<RecipeDto>(updatedRecipe);
    }
}

