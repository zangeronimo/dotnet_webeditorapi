
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;

public sealed record CreateLevelRequest(
    string Name,
    Status Active,
    RequestContext Context
) : ApplicationRequest(Context);