using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.RecipeRatings;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.RecipeRatings;

public class CreateRecipeRatingUC(IRecipeRatingRepository recipeRatingRepository, IMapper mapper) : IUseCase<CreateRecipeRatingRequest, RecipeRatingDto>
{
    private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RecipeRatingDto> ExecuteAsync(CreateRecipeRatingRequest request)
    {
        RecipeRating newRecipeRating = new RecipeRating(request.Score, request.Name, request.Comment, request.Status, request.RecipeId, request.Context.CompanyId);
        await _recipeRatingRepository.AddAsync(newRecipeRating);
        RecipeRating? createdRecipeRating = await _recipeRatingRepository.GetByIdAsync(newRecipeRating.Id, request.Context.CompanyId);
        return _mapper.Map<RecipeRatingDto>(createdRecipeRating);
    }
}
