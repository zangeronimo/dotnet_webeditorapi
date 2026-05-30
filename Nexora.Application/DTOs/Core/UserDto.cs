using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class UserDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Status Status { get; set; }
}
