using System;

namespace Nexora.Application.DTOs.Core;

public class ModuleAccessDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
