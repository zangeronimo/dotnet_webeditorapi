namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeIngredient(string description)
{
    public string Description { get; } = description;
}
