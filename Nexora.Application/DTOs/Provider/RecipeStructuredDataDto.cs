using System.Text.Json.Serialization;

namespace Nexora.Infrastructure.Provider.StructuredData;

public sealed class RecipeStructuredDataDto
{
    [JsonPropertyName("@context")]
    public string Context { get; set; } = "https://schema.org";

    [JsonPropertyName("@type")]
    public string Type { get; set; } = "Recipe";

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("image")]
    public string[]? Image { get; set; }

    [JsonPropertyName("author")]
    public AuthorDto Author { get; set; }

    [JsonPropertyName("datePublished")]
    public string? DatePublished { get; set; }

    [JsonPropertyName("prepTime")]
    public string? PrepTime { get; set; }

    [JsonPropertyName("cookTime")]
    public string? CookTime { get; set; }

    [JsonPropertyName("totalTime")]
    public string? TotalTime { get; set; }

    [JsonPropertyName("recipeYield")]
    public string? RecipeYield { get; set; }

    [JsonPropertyName("recipeCategory")]
    public string RecipeCategory { get; set; }

    [JsonPropertyName("recipeCuisine")]
    public string RecipeCuisine { get; set; }

    [JsonPropertyName("keywords")]
    public string Keywords { get; set; }

    [JsonPropertyName("recipeIngredient")]
    public List<string> RecipeIngredient { get; set; }

    [JsonPropertyName("recipeInstructions")]
    public List<HowToStepDto> RecipeInstructions { get; set; }
}

public sealed class AuthorDto
{
    [JsonPropertyName("@type")]
    public string Type { get; set; } = "Organization";

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public sealed class HowToStepDto
{
    [JsonPropertyName("@type")]
    public string Type { get; set; } = "HowToStep";

    [JsonPropertyName("text")]
    public string Text { get; set; }
}