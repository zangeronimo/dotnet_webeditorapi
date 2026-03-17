using System.Text.Json.Serialization;

namespace WEBEditorAPI.Application.DTOs.System;


public class AuthResponse
{
    public string Token { get; set; } = null!;
    [JsonIgnore]
    public string RefreshToken { get; set; } = null!;
}

