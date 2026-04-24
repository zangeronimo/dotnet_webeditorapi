using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Ratings;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Ratings;

public class UpdateRatingUC(IRatingRepository ratingRepository, IMapper mapper) : IUseCase<UpdateRatingRequest, RatingDto>
{
    private readonly IRatingRepository _ratingRepository = ratingRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RatingDto> ExecuteAsync(UpdateRatingRequest request)
    {
        Rating? updateRating = await _ratingRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateRating == null)
            throw new ApiBadRequestException("Rating não encontrada.");
        updateRating.Update(request.Name, request.Rate, request.Comment, request.Active);
        await _ratingRepository.UpdateAsync(updateRating);
        Rating? updatedRating = await _ratingRepository.GetByIdAsync(updateRating.Id, updateRating.CompanyId);
        return _mapper.Map<RatingDto>(updatedRating);
    }
}
