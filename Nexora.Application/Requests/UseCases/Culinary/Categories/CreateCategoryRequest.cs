using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Application.Requests.UseCases.Culinary.Categories;

public sealed record CreateCategoryRequest(
    CategoryName Name,
    string? Description,
    Guid? ParentId,
    int DisplayOrder,
    Status Status,
    CategorySeo Seo,
    RequestContext Context
) : ApplicationRequest(Context);