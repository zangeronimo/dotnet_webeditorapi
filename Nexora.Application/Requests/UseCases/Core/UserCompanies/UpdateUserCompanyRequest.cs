using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.UserCompanies;

public sealed record UpdateUserCompanyRequest(
    Guid Id,
    string? NickName,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
