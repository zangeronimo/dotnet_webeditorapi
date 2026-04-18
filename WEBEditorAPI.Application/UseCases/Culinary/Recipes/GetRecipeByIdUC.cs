using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Recipes;

public class GetRecipeByIdUC(IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<GetByIdRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<RecipeDto> ExecuteAsync(GetByIdRequest request)
    {
        Recipe? recipe = await _recipeRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (recipe == null)
            throw new ApiNotFoundException("Receita não encontrada");
        return _mapper.Map<RecipeDto>(recipe);
    }
}
