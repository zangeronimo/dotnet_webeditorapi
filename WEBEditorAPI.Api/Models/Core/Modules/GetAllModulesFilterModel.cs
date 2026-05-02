using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Core.Modules;

public class GetAllModulesFilterModel : PaginationModel
{
    public string OrderBy { get; init; } = "Id";
    public bool Desc { get; init; } = false;
    public string? Name { get; init; }
    public Status? Status { get; init; }
}
