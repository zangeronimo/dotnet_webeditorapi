using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.System.Roles;

public sealed record CreateRoleRequest(
    string Name,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
