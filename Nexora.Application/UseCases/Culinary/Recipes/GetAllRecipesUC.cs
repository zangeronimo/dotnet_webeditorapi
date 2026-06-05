using AutoMapper;

using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Recipes;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Recipes;

public class GetAllRecipesUC(IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<RecipeDto>> ExecuteAsync(GetAllRecipesFilterRequest request)
    {
        (IEnumerable<Recipe> recipes, int total) = await _recipeRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Status, request.CategoryId, request.Context.CompanyId);

        return new PaginationResult<RecipeDto>
        {
            Items = _mapper.Map<IEnumerable<RecipeDto>>(recipes),
            Total = total
        };
    }
}
