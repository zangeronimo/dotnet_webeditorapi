
namespace Nexora.Application.Requests.UseCases.System.UserCompanies;

public sealed record CreateUserCompanyRequest(
    string Email,
    RequestContext Context
) : ApplicationRequest(Context);
