namespace WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;

public sealed record UpdateUserCompanyModulesRequest(
    Guid UserCompanyId,
    List<ModuleRoleRequest> Roles,
    RequestContext Context
) : ApplicationRequest(Context);
