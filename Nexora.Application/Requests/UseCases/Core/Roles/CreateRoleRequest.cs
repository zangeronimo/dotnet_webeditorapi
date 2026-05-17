using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Roles;

public sealed record CreateRoleRequest(
    string Name,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
