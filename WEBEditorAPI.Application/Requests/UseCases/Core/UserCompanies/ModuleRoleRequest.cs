namespace WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;

public sealed record ModuleRoleRequest(
    Guid ModuleId,
    Guid RoleId
);
