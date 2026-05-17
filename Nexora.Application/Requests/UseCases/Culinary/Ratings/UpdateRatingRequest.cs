using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Ratings;

public sealed record UpdateRatingRequest(
    Guid Id,
    int Rate,
    string? Name,
    string? Comment,
    Status Active,
    RequestContext Context
) : ApplicationRequest(Context);
