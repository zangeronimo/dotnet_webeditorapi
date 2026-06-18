using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class GenerateApiClientSecretDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public ApiClientStatus Status { get; set; }
}
