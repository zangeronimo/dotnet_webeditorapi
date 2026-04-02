using WEBEditorAPI.Application.DTOs.System;

namespace WEBEditorAPI.Application.Interfaces;

public interface IRefreshToken
{
    Task<AuthResponse> ExecuteAsync(string refresh);
}
