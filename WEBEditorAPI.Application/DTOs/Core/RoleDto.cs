using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Core;

public class RoleDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public List<PermissionDto> Permissions { get; set; } = [];
}
