using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects;
using WEBEditorAPI.Domain.ValueObjects.Culinary;

namespace WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;

public sealed record CreateRecipeRequest(
    string Name,
    RecipeContent Content,
    RecipeTiming Timing,
    RecipeYield Yield,
    RecipeAttributes Attributes,
    RecipeSeo Seo,
    RecipeEngagement Engagement,
    Guid LevelId,
    RequestContext Context
) : ApplicationRequest(Context);
