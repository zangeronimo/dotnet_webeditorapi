using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Companies;

public sealed record GetAllCompaniesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Status,
    RequestContext Context) : ApplicationRequest(Context);
