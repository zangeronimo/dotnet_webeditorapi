using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Modules;

public sealed record UpdateModuleRequest(
    Guid Id,
    string Name,
    Status Active,
    List<PermissionDto> PermissionsDtos,
    RequestContext Context
) : ApplicationRequest(Context);
