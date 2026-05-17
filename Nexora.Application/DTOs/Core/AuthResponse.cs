using System.Text.Json.Serialization;

namespace Nexora.Application.DTOs.Core;


public class AuthResponse
{
    public string Token { get; set; } = null!;
    [JsonIgnore]
    public string RefreshToken { get; set; } = null!;
}

