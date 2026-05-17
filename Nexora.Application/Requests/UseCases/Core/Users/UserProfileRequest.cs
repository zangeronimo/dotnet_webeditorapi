namespace Nexora.Application.Requests.UseCases.Core.Users;

public sealed record UserProfileRequest(
    string Name,
    string Email,
    string? Password,
    string NickName,
    RequestContext Context
) : ApplicationRequest(Context);