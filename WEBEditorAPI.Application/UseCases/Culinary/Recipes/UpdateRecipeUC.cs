using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.DTOs.JsonLd;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.JsonLd;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Domain.ValueObjects;
using WEBEditorAPI.Domain.ValueObjects.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Recipes;

public class UpdateRecipeUC(IRecipeRepository recipeRepository, ILevelRepository levelRepository, IStorageProvider storageProvider, IJsonLdProvider<RecipeJsonLdRequest, RecipeJsonLd> recipeJsonProvider, IMapper mapper) : IUseCase<UpdateRecipeRequest, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly ILevelRepository _levelRepository = levelRepository;
    private readonly IStorageProvider _storageProvider = storageProvider;
    private readonly IJsonLdProvider<RecipeJsonLdRequest, RecipeJsonLd> _recipeJsonLdProvider = recipeJsonProvider;
    private readonly IMapper _mapper = mapper;

    public async Task<RecipeDto> ExecuteAsync(UpdateRecipeRequest request)
    {
        var slug = Slug.Restore(request.Slug);
        Recipe? recipe = await _recipeRepository.GetBySlugAsync(slug.Value, request.Context.CompanyId);
        if (recipe != null && recipe.Id != request.Id)
            throw new ApiBadRequestException("Receita já cadastrada com esse slug");
        Recipe? updateRecipe = await _recipeRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateRecipe == null)
            throw new ApiBadRequestException("Receita não encontrada");
        var media = new RecipeMedia("");
        if (!string.IsNullOrEmpty(request.Image))
        {
            if (!string.IsNullOrEmpty(updateRecipe.Media.ImageUrl))
                await _storageProvider.DeleteFileAsync(updateRecipe.Media.ImageUrl);
            var imageUrl = await _storageProvider.SaveFileAsync(request.Image, request.Context.CompanyId.ToString());
            media = new RecipeMedia(imageUrl);
        }
        updateRecipe.Update(slug, request.Name, request.Content, request.Timing, request.Yield, request.Attributes, media, request.Seo, request.Active, request.LevelId);
        string jsonLdString = await GenerateJsonLd(updateRecipe);
        updateRecipe.SetSchemaJsonLd(jsonLdString);
        await _recipeRepository.UpdateAsync(updateRecipe);
        Recipe? updatedRecipe = await _recipeRepository.GetByIdAsync(updateRecipe.Id, updateRecipe.CompanyId);
        return _mapper.Map<RecipeDto>(updatedRecipe);
    }

    private async Task<string> GenerateJsonLd(Recipe updateRecipe)
    {
        Level? recipeLevel = await _levelRepository.GetByIdAsync(updateRecipe.LevelId, updateRecipe.CompanyId);
        if (recipeLevel == null)
            throw new ApiBadRequestException("Level não encontrado.");
        var jsonLdRequest = new RecipeJsonLdRequest(updateRecipe, recipeLevel);
        var jsonLdObject = _recipeJsonLdProvider.Generate(jsonLdRequest);
        var jsonLdString = JsonSerializer.Serialize(jsonLdObject, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
        return jsonLdString;
    }
}
