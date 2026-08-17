using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.RecipeRatings;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.Errors.Culiarny;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.RecipeRatings;

public class UpdateRecipeRatingUC(IRecipeRatingRepository recipeRatingRepository, IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<UpdateRecipeRatingRequest, RecipeRatingDto>
{
    private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RecipeRatingDto> ExecuteAsync(UpdateRecipeRatingRequest request)
    {
        RecipeRating? updateRecipeRating = await _recipeRatingRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateRecipeRating == null)
            throw new ApiBadRequestException(RecipeRatingErrors.NotFound);
        if (request.Status == CulinaryRecipeRatingStatus.Active && updateRecipeRating.PublishedAt is null)
        {
            Recipe? updateRecipe = await _recipeRepository.GetByIdAsync(request.RecipeId, request.Context.CompanyId);
            if (updateRecipe == null)
                throw new ApiBadRequestException(RecipeErrors.NotFound);
            updateRecipe.AddRating(request.Score.Value);
            await _recipeRepository.UpdateAsync(updateRecipe);
        }
        updateRecipeRating.Update(request.Score, request.Name, request.Comment, request.Status);
        await _recipeRatingRepository.UpdateAsync(updateRecipeRating);
        RecipeRating? updatedRecipeRating = await _recipeRatingRepository.GetByIdAsync(updateRecipeRating.Id, request.Context.CompanyId);
        return _mapper.Map<RecipeRatingDto>(updatedRecipeRating);
    }
}
