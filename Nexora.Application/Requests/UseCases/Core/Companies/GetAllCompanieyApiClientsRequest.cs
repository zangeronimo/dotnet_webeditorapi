namespace Nexora.Application.Requests.UseCases.Core.Companies;

public sealed record GetAllCompanyApiClientsRequest(
    Guid CompanyId,
    RequestContext Context) : ApplicationRequest(Context);
