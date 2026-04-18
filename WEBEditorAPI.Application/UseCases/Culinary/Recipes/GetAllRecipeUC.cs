using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Recipes;

public class GetAllRecipeUC(IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<RecipeDto>> ExecuteAsync(GetAllRecipesFilterRequest request)
    {
        (IEnumerable<Recipe> recipes, int total) = await _recipeRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Active, request.Context.CompanyId);

        return new PaginationResult<RecipeDto>
        {
            Items = _mapper.Map<IEnumerable<RecipeDto>>(recipes),
            Total = total
        };
    }
}
