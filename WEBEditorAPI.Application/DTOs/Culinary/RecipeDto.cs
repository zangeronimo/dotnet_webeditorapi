using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Culinary;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string FullDescription { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
    public string Preparation { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int RestTime { get; set; }
    public string YieldTotal { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string Tools { get; set; } = string.Empty;
    public string Cuisine { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public int Views { get; set; }
    public int Likes { get; set; }
    public Status Active { get; set; }
}
