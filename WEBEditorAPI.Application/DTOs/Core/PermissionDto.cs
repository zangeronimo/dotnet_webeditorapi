using System;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Core;

public class PermissionDto : BaseDto
{
    public string Code { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public Status Status { get; set; }
}
