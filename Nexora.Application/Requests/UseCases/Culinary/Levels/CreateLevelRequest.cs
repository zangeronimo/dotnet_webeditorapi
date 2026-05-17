using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Levels;

public sealed record CreateLevelRequest(
    string Name,
    Status Active,
    RequestContext Context
) : ApplicationRequest(Context);