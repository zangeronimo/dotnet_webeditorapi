using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Core.Users;

public class GetAllUsersFilterModel : PaginationModel
{
    public string OrderBy { get; init; } = "Id";
    public bool Desc { get; init; } = false;
    public string? Name { get; init; }
    public string? Email { get; init; }
    public Status? Status { get; init; }
}
