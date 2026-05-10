using System;

namespace WEBEditorAPI.Application.DTOs.Core;

public class ModuleAccessDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
