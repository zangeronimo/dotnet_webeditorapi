using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culiarny;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Recipes;

public class DeleteRecipeUC(IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<DeleteRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RecipeDto> ExecuteAsync(DeleteRequest request)
    {
        Recipe? recipe = await _recipeRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (recipe == null)
            throw new ApiNotFoundException(RecipeErrors.NotFound);
        recipe.Delete();
        await _recipeRepository.UpdateAsync(recipe);
        return _mapper.Map<RecipeDto>(recipe);
    }
}
