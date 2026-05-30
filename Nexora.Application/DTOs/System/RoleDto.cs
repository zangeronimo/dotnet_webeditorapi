using Nexora.Application.DTOs.Core;
using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.System;

public class RoleDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public List<PermissionDto> Permissions { get; set; } = [];
}
