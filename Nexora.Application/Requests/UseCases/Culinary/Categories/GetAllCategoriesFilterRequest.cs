using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Categories;

public sealed record GetAllCategoriesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Status,
    RequestContext Context) : ApplicationRequest(Context);
