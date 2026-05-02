using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Requests.UseCases.Core;

namespace WEBEditorAPI.Application.Interfaces;

public interface IMakeLogin
{
    Task<AuthResponse> ExecuteAsync(AuthRequest request);
}
