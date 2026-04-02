using WEBEditorAPI.Application.DTOs;

namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public sealed record GetAllUsersFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    string? Email,
    RequestContext Context) : ApplicationRequest(Context);
