using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Core.UserCompanies;

public class GetAllUserCompaniesFilterModel : PaginationModel
{
    public string OrderBy { get; init; } = "Id";
    public bool Desc { get; init; } = false;
    public string? NickName { get; init; }
    public Status? Status { get; init; }
}
