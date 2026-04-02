namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public sealed record DeleteUserRequest(Guid UserId, RequestContext Context) : ApplicationRequest(Context);
