namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeSection(
    string? title,
    IReadOnlyCollection<RecipeIngredient> ingredients,
    IReadOnlyCollection<RecipeHowToStep> steps)
{
    public string? Title { get; } = string.IsNullOrWhiteSpace(title)
            ? null
            : title.Trim();
    public IReadOnlyCollection<RecipeIngredient> Ingredients { get; } = ingredients;
    public IReadOnlyCollection<RecipeHowToStep> Steps { get; } = steps;
}