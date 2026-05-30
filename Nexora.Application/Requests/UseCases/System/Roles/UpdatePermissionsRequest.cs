namespace Nexora.Application.Requests.UseCases.System.Roles;

public sealed record UpdatePermissionsRequest(
    Guid RoleId,
    List<Guid> PermissionIds,
    RequestContext Context
) : ApplicationRequest(Context);
