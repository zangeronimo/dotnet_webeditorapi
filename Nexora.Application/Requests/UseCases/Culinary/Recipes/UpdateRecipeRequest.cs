using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Application.Requests.UseCases.Culinary.Recipes;

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
    string? Image,
    RequestContext Context
) : ApplicationRequest(Context);

