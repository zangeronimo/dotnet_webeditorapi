using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Companies;

public sealed record CreateCompanyRequest(
    string Name,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
