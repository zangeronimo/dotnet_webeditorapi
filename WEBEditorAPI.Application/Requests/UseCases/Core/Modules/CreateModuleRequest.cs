using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Core.Modules;

public sealed record CreateModuleRequest(
    string Name,
    Status Status,
    RequestContext Context
) : ApplicationRequest(Context);
