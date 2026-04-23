using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Ratings;

public class GetRatingByIdUC(IRatingRepository ratingRepository, IMapper mapper) : IUseCase<GetByIdRequest, RatingDto>
{
    private readonly IRatingRepository _ratingRepository = ratingRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<RatingDto> ExecuteAsync(GetByIdRequest request)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (rating == null)
            throw new ApiNotFoundException("Rating não encontrado");
        return _mapper.Map<RatingDto>(rating);
    }
}
