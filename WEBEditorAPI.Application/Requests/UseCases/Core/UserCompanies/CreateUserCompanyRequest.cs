
namespace WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;

public sealed record CreateUserCompanyRequest(
    string Email,
    RequestContext Context
) : ApplicationRequest(Context);
