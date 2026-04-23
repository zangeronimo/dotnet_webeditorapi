

using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Ratings;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Ratings;

public class GetAllRatingUC(IRatingRepository ratingRepository, IMapper mapper) : IUseCase<GetAllRatingsFilterRequest, PaginationResult<RatingDto>>
{
    private readonly IRatingRepository _ratingRepository = ratingRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<RatingDto>> ExecuteAsync(GetAllRatingsFilterRequest request)
    {
        (IEnumerable<Rating> ratings, int total) = await _ratingRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Active, request.Context.CompanyId);

        return new PaginationResult<RatingDto>
        {
            Items = _mapper.Map<IEnumerable<RatingDto>>(ratings),
            Total = total
        };
    }
}
