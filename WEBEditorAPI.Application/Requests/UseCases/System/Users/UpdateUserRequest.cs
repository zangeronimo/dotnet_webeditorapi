using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Application.Requests.UseCases.System.Users;

public sealed record UpdateUserRequest(
    Guid Id,
    string Name,
    string Email,
    string? Password,
    List<Role> Roles,
    RequestContext Context
) : ApplicationRequest(Context);