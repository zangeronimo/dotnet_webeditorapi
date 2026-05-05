using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Roles;

public sealed record UpdateRoleRequest(
    Guid Id,
    string Name,
    Status Active,
    RequestContext Context
) : ApplicationRequest(Context);
