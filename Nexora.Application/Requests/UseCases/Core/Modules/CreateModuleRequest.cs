using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Modules;

public sealed record CreateModuleRequest(
    string Name,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
