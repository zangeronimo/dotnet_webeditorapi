using WEBEditorAPI.Application.DTOs;

namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public record CreateUserRequest(
    string Name,
    string Email,
    string Password,
    RequestContext Context
) : ApplicationRequest(Context);