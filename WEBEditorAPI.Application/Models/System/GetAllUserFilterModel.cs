using WEBEditorAPI.Application.DTOs;

namespace WEBEditorAPI.Application.Models.System;

public class GetAllUserFilterModel : PaginationRequest
{

    public string? OrderBy { get; set; } = "Id";
    public Boolean Desc { get; set; } = false;
    public string? Name { get; set; }
    public string? Email { get; set; }

    public Guid CompanyId { get; set; }

}
