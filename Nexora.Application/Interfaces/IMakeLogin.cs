using Nexora.Application.DTOs.Core;
using Nexora.Application.Requests.UseCases.Core;

namespace Nexora.Application.Interfaces;

public interface IMakeLogin
{
    Task<AuthResponse> ExecuteAsync(AuthRequest request);
}
