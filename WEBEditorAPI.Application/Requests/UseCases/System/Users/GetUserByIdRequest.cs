namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public sealed record GetUserByIdRequest(Guid UserId, RequestContext Context) : ApplicationRequest(Context);
