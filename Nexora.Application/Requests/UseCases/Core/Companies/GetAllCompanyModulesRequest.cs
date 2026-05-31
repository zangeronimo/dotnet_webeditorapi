namespace Nexora.Application.Requests.UseCases.Core.Companies;

public sealed record GetAllCompanyModulesRequest(
    RequestContext Context
) : ApplicationRequest(Context);
