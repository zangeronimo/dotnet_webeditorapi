using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Companies;

public sealed record UpdateCompanyRequest(
    Guid Id,
    string Name,
    Status Active,
    RequestContext Context
) : ApplicationRequest(Context);
