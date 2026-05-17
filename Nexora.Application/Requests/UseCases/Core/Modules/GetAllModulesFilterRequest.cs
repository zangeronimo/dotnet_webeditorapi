using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Modules;

public sealed record GetAllModulesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Status,
    RequestContext Context) : ApplicationRequest(Context);
