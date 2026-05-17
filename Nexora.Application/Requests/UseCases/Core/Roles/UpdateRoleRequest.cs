using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Roles;

public sealed record UpdateRoleRequest(
    Guid Id,
    string Name,
    Status Active,
    RequestContext Context
) : ApplicationRequest(Context);
