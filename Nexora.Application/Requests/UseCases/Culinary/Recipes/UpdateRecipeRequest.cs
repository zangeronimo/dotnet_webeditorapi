using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Application.Requests.UseCases.Culinary.Recipes;

public sealed record UpdateRecipeRequest(
    Guid Id,
    string Name,
    RecipeContent Content,
    RecipeTiming Timing,
    RecipeYield Yield,
    RecipeAttributes Attributes,
    RecipeSeo Seo,
    IReadOnlyCollection<Guid> TagIds,
    Status Status,
    Guid CategoryId,
    RequestContext Context
) : ApplicationRequest(Context);
