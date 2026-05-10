using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;

public sealed record UpdateUserCompanyRequest(
    Guid Id,
    string? NickName,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
