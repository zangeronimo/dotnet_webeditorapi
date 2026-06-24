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

public class UpdateRecipeUC(IRecipeRepository recipeRepository, IRecipeStructuredDataProvider structuredData, IMapper mapper) : IUseCase<UpdateRecipeRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IRecipeStructuredDataProvider _structuredData = structuredData;
    private readonly IMapper _mapper = mapper;
    public async Task<RecipeDto> ExecuteAsync(UpdateRecipeRequest request)
    {
        var slug = Slug.Create(request.Name);
        Recipe? recipe = await _recipeRepository.GetBySlugAsync(slug, request.Context.CompanyId);
        if (recipe != null && recipe.Id != request.Id)
            throw new ApiBadRequestException(RecipeErrors.AlreadyExists);
        Recipe? updateRecipe = await _recipeRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateRecipe == null)
            throw new ApiBadRequestException(RecipeErrors.NotFound);
        updateRecipe.Update(
            slug,
            request.Name,
            request.Content,
            request.Timing,
            request.Yield,
            request.Attributes,
            request.Seo,
            request.Status,
            request.CategoryId);
        updateRecipe.SetTags(request.TagIds);
        var structuredData = _structuredData.Generate(updateRecipe, updateRecipe.Category, []);
        updateRecipe.SetStructuredData(structuredData);
        await _recipeRepository.UpdateAsync(updateRecipe);
        Recipe? updatedRecipe = await _recipeRepository.GetByIdAsync(updateRecipe.Id, request.Context.CompanyId);
        return _mapper.Map<RecipeDto>(updatedRecipe);
    }
}
