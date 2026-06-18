using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class ApiClientDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public CulinaryRecipeDifficulty Status { get; set; }
}
