using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class ModuleDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>() { };
}
