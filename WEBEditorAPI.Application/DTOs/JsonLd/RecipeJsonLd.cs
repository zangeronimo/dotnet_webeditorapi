using System.Text.Json.Serialization;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Application.DTOs.JsonLd;

public class RecipeJsonLd
{

    [JsonPropertyName("@context")]
    public string Context { get; set; } = "https://schema.org";

    [JsonPropertyName("@type")]
    public string Type { get; set; } = "Recipe";
    public string Name { get; set; } = string.Empty;
    public AuthorJsonLd? Author { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Image { get; set; } = [];
    public string? TotalTime { get; set; }
    public string? PrepTime { get; set; }
    public string? CookTime { get; set; }
    public string RecipeYield { get; set; } = string.Empty;
    public string RecipeCategory { get; set; } = string.Empty;
    public string RecipeCuisine { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public List<string> RecipeIngredient { get; set; } = [];
    public List<HowToInstructionJsonLd> RecipeInstructions { get; set; } = [];
    public List<string> Tool { get; set; } = [];
    public string? DatePublished { get; set; }
}

public class AuthorJsonLd
{
    [JsonPropertyName("@type")]
    public string Type { get; set; } = "Organization";

    public string Name { get; set; } = string.Empty;
}

public class HowToInstructionJsonLd
{
    [JsonPropertyName("@type")]
    public string Type { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}