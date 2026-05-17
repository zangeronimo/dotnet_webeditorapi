using System;
using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class PermissionDto : BaseDto
{
    public string Code { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public Status Status { get; set; }
}
