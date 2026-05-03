using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Companies;

public sealed record CreateCompanyRequest(
    string Name,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
