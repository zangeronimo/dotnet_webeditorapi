using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Core;

public class ModuleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>() { };
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
