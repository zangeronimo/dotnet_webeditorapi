using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Users;

public sealed record GetAllUsersFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    string? Email,
    Status? Status,
    RequestContext Context) : ApplicationRequest(Context);
