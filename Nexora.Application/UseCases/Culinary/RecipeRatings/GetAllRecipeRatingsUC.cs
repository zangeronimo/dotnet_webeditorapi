using AutoMapper;

using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.RecipeRatings;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.RecipeRatings;

public class GetAllRecipeRatingsUC(IRecipeRatingRepository recipeRatingRepository, IMapper mapper) : IUseCase<GetAllRecipeRatingsFilterRequest, PaginationResult<RecipeRatingDto>>
{
    private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<RecipeRatingDto>> ExecuteAsync(GetAllRecipeRatingsFilterRequest request)
    {
        (IEnumerable<RecipeRating> recipeRatings, int total) = await _recipeRatingRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Status, request.Context.CompanyId);

        return new PaginationResult<RecipeRatingDto>
        {
            Items = _mapper.Map<IEnumerable<RecipeRatingDto>>(recipeRatings),
            Total = total
        };
    }
}
