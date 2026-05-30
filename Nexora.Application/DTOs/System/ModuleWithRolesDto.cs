namespace Nexora.Application.DTOs.System;

public class ModuleWithRolesDto
{
    public ModuleAccessDto Module { get; set; } = default!;
    public Guid? SelectedRoleId { get; set; }
    public List<RoleAccessDto> Roles { get; set; } = [];
}