namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public sealed record UpdateUserRequest(
    Guid Id,
    string Name,
    string Email,
    string? Password,
    List<Guid> RoleIds,
    RequestContext Context
) : ApplicationRequest(Context);