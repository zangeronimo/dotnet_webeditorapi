namespace Nexora.Application.Requests.UseCases.Core.Companies;

public sealed record CompanyProfileRequest(
    string Name,
    RequestContext Context
) : ApplicationRequest(Context);
