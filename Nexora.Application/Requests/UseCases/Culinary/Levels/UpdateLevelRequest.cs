using Nexora.Application.DTOs.Culinary;
using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Levels;

public sealed record UpdateLevelRequest(
    Guid Id,
    string Slug,
    string Name,
    Status Active,
    List<CategoryDto> CategoriesDtos,
    RequestContext Context
) : ApplicationRequest(Context);
