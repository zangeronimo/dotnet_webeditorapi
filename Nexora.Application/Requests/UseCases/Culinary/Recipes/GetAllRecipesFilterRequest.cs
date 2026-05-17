using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Recipes;

public sealed record GetAllRecipesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Active,
    RequestContext Context) : ApplicationRequest(Context);