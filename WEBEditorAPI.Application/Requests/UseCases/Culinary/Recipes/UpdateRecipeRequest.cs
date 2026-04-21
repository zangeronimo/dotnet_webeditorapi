using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects.Culinary;

namespace WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;

public sealed record UpdateRecipeRequest(
    Guid Id,
    string Slug,
    string Name,
    RecipeContent Content,
    RecipeTiming Timing,
    RecipeYield Yield,
    RecipeAttributes Attributes,
    RecipeSeo Seo,
    Guid LevelId,
    Status Active,
    RequestContext Context
) : ApplicationRequest(Context);

