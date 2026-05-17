using Nexora.Application.DTOs.Core;

namespace Nexora.Application.Interfaces;

public interface IRefreshToken
{
    Task<AuthResponse> ExecuteAsync(string refresh);
}
