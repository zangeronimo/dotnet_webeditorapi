using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Culinary.Categories;

public sealed record GetAllCategoriesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Active,
    RequestContext Context) : ApplicationRequest(Context);
