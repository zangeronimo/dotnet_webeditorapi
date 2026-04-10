using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;

public sealed record UpdateLevelRequest(
    Guid Id,
    string Slug,
    string Name,
    Status Active,
    List<CategoryDto> CategoriesDtos,
    RequestContext Context
) : ApplicationRequest(Context);
