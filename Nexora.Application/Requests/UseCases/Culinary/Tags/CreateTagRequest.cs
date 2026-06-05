using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Tags;

public sealed record CreateTagRequest(
    string Name,
    string? Description,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);