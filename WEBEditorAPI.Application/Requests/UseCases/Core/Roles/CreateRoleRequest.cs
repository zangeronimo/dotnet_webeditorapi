using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Roles;

public sealed record CreateRoleRequest(
    string Name,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
