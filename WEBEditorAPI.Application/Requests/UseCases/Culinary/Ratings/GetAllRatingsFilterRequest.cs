using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Culinary.Ratings;

public sealed record GetAllRatingsFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Active,
    RequestContext Context) : ApplicationRequest(Context);