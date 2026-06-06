using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.RecipeRatings;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.RecipeRatings;

public class UpdateRecipeRatingUC(IRecipeRatingRepository recipeRatingRepository, IMapper mapper) : IUseCase<UpdateRecipeRatingRequest, RecipeRatingDto>
{
    private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RecipeRatingDto> ExecuteAsync(UpdateRecipeRatingRequest request)
    {
        RecipeRating? updateRecipeRating = await _recipeRatingRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateRecipeRating == null)
            throw new ApiBadRequestException(RecipeRatingErrors.NotFound);
        updateRecipeRating.Update(request.Score, request.Name, request.Comment, request.Status);
        await _recipeRatingRepository.UpdateAsync(updateRecipeRating);
        RecipeRating? updatedRecipeRating = await _recipeRatingRepository.GetByIdAsync(updateRecipeRating.Id, request.Context.CompanyId);
        return _mapper.Map<RecipeRatingDto>(updatedRecipeRating);
    }
}
