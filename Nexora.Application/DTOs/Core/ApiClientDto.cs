using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class ApiClientDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
}
