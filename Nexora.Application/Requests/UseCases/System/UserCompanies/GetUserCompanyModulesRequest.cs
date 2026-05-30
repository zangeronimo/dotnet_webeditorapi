namespace Nexora.Application.Requests.UseCases.System.UserCompanies;

public sealed record GetUserCompanyModulesRequest(Guid ResourceId, RequestContext Context) : ApplicationRequest(Context);
