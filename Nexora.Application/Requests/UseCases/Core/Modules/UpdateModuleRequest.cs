using Nexora.Application.DTOs.Core;
using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Core.Modules;

public sealed record UpdateModuleRequest(
    Guid Id,
    string Name,
    Status Active,
    List<PermissionDto> PermissionsDtos,
    RequestContext Context
) : ApplicationRequest(Context);
