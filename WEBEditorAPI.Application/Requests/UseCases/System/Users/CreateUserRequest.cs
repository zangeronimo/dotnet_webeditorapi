namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public sealed record CreateUserRequest(
    string Name,
    string Email,
    string Password,
    RequestContext Context
) : ApplicationRequest(Context);