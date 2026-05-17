namespace Nexora.Application.Requests.UseCases.Core.Users;

public sealed record GetUserProfileRequest(
    RequestContext Context
) : ApplicationRequest(Context);