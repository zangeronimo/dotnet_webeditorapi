using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Requests.UseCases.System;

namespace WEBEditorAPI.Application.Interfaces;

public interface IMakeLogin
{
    Task<AuthResponse> ExecuteAsync(AuthRequest request);
}
