using WEBEditorAPI.Application.DTOs;

namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public record DeleteUserRequest(Guid UserId, RequestContext Context) : ApplicationRequest(Context);
