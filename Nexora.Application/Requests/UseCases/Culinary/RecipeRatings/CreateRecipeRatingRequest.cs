using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Application.Requests.UseCases.Culinary.RecipeRatings;

public sealed record CreateRecipeRatingRequest(
    RecipeScore Score,
    string? Name,
    string? Comment,
    CulinaryRecipeRatingStatus Status,
    Guid RecipeId,
    RequestContext Context
) : ApplicationRequest(Context);
