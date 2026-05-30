namespace Nexora.Application.Requests.UseCases.System.UserCompanies;

public sealed record UpdateUserCompanyModulesRequest(
    Guid UserCompanyId,
    List<ModuleRoleRequest> Roles,
    RequestContext Context
) : ApplicationRequest(Context);
