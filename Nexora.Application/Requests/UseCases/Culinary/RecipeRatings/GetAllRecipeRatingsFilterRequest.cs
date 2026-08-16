using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.RecipeRatings;


public sealed record GetAllRecipeRatingsFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    CulinaryRecipeRatingStatus? Status,
    RequestContext Context) : ApplicationRequest(Context);