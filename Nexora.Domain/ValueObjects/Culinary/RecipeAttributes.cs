using Nexora.Domain.Enums;

namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeAttributes(CulinaryRecipeDifficulty difficulty, string cuisine)
{
    public CulinaryRecipeDifficulty Difficulty { get; } = difficulty;
    public string Cuisine { get; } = cuisine;
}
