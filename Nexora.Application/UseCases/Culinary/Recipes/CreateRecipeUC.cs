using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Recipes;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culiarny;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Domain.ValueObjects;

namespace Nexora.Application.UseCases.Culinary.Recipes;

public class CreateRecipeUC(IRecipeRepository recipeRepository, IMapper mapper) : IUseCase<CreateRecipeRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<RecipeDto> ExecuteAsync(CreateRecipeRequest request)
    {
        var slug = Slug.Create(request.Name);
        Recipe? recipe = await _recipeRepository.GetBySlugAsync(slug, request.Context.CompanyId);
        if (recipe != null)
            throw new ApiBadRequestException(RecipeErrors.AlreadyExists);
        Recipe newRecipe = new Recipe(
            slug,
            request.Name,
            request.Content,
            request.Timing,
            request.Yield,
            request.Attributes,
            request.Seo,
            request.CategoryId,
            request.Context.CompanyId);
        newRecipe.SetTags(request.TagIds);
        await _recipeRepository.AddAsync(newRecipe);
        Recipe? createdRecipe = await _recipeRepository.GetByIdAsync(newRecipe.Id, request.Context.CompanyId);
        return _mapper.Map<RecipeDto>(createdRecipe);
    }
}

