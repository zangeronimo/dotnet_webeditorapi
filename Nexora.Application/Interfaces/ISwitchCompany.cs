using Nexora.Application.DTOs.Core;
using Nexora.Application.Requests;

namespace Nexora.Application.Interfaces;

public interface ISwitchCompany
{
    Task<AuthResponse> ExecuteAsync(RequestContext context);
}
