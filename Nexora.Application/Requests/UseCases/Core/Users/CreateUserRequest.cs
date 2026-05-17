using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Users;

public sealed record CreateUserRequest(
    string Name,
    string Email,
    string Password,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);