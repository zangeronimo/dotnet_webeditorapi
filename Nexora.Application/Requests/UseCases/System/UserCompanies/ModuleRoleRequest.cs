namespace Nexora.Application.Requests.UseCases.System.UserCompanies;

public sealed record ModuleRoleRequest(
    Guid ModuleId,
    Guid RoleId
);
