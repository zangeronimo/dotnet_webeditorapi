namespace WEBEditorAPI.Application.DTOs.System;


public class AuthResponse
{
    public UserResponse User { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

