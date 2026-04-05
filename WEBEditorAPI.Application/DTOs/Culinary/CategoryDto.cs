using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Culinary;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Status Active { get; set; }
}
