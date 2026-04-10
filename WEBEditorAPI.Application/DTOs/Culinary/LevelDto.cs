using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Culinary;

public class LevelDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Status Active { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
}
