using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.RecipeRatings;

public class GetRecipeRatingByIdUC(IRecipeRatingRepository recipeRatingRepository, IMapper mapper) : IUseCase<GetByIdRequest, RecipeRatingDto>
{
    private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RecipeRatingDto> ExecuteAsync(GetByIdRequest request)
    {
        RecipeRating? recipeRating = await _recipeRatingRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (recipeRating == null)
            throw new ApiNotFoundException(RecipeRatingErrors.NotFound);
        return _mapper.Map<RecipeRatingDto>(recipeRating);
    }
}
