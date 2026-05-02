using WEBEditorAPI.Application.DTOs.Core;

namespace WEBEditorAPI.Application.Interfaces;

public interface IRefreshToken
{
    Task<AuthResponse> ExecuteAsync(string refresh);
}
