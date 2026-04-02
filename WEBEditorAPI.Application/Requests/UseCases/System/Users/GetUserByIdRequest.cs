using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.Requests;

namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public record GetUserByIdRequest(Guid UserId, RequestContext Context) : ApplicationRequest(Context);
