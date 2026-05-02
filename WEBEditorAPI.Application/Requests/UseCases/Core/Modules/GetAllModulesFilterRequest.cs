using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Modules;

public sealed record GetAllModulesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Active,
    RequestContext Context) : ApplicationRequest(Context);
