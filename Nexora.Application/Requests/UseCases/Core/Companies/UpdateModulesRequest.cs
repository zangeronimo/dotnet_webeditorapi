namespace Nexora.Application.Requests.UseCases.Core.Companies;

public sealed record UpdateModulesRequest(
    Guid CompanyId,
    List<Guid> ModuleIds,
    RequestContext Context
) : ApplicationRequest(Context);
