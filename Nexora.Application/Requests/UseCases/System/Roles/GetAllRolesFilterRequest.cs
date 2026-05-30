using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.System.Roles;

public sealed record GetAllRolesFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Status,
    RequestContext Context) : ApplicationRequest(Context);