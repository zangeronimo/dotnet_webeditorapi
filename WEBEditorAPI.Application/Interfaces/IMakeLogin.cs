using WEBEditorAPI.Application.DTOs.System;

namespace WEBEditorAPI.Application.Interfaces;

public interface IMakeLogin
{
    Task<AuthResponse> ExecuteAsync(AuthRequest request);
}
