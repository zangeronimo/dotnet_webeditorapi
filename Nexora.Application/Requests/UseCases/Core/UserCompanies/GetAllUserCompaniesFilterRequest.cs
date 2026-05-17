using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.UserCompanies;

public sealed record GetAllUserCompaniesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? NickName,
    Status? Status,
    RequestContext Context) : ApplicationRequest(Context);
