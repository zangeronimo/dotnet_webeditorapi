namespace Nexora.Application.Requests.UseCases.Core.Roles;

public sealed record UpdatePermissionsRequest(
    Guid RoleId,
    List<Guid> PermissionIds,
    RequestContext Context
) : ApplicationRequest(Context);
