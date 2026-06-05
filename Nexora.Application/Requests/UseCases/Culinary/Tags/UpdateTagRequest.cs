using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Tags;

public sealed record UpdateTagRequest(
    Guid Id,
    string Name,
    string? Description,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);