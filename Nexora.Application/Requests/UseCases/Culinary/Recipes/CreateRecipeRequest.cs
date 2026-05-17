using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Application.Requests.UseCases.Culinary.Recipes;

public sealed record CreateRecipeRequest(
    string Name,
    RecipeContent Content,
    RecipeTiming Timing,
    RecipeYield Yield,
    RecipeAttributes Attributes,
    RecipeSeo Seo,
    Guid LevelId,
    RequestContext Context
) : ApplicationRequest(Context);
