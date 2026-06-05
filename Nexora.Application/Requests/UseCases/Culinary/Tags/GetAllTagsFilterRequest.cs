using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Tags;

public sealed record GetAllTagsFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Status,
    RequestContext Context) : ApplicationRequest(Context);
