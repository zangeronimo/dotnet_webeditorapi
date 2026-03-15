using System.Text.Json.Serialization;

namespace WEBEditorAPI.Application.DTOs.System;

public class AuthRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = null!;
}
