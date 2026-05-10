using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Core.UserCompanies;

public class GetAllUserCompaniesFilterModel : PaginationModel
{
    public string OrderBy { get; init; } = "Id";
    public bool Desc { get; init; } = false;
    public string? NickName { get; init; }
    public Status? Status { get; init; }
}
