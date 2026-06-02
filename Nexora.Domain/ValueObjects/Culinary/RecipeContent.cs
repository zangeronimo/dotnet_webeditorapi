namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeContent(
    string shortDescription,
    string fullDescription,
    IReadOnlyCollection<RecipeIngredient> ingredients,
    IReadOnlyCollection<RecipeHowToStep> steps,
    string notes)
{
    public string ShortDescription { get; } = shortDescription;
    public string FullDescription { get; } = fullDescription;

    public IReadOnlyCollection<RecipeIngredient> Ingredients { get; } = ingredients;

    public IReadOnlyCollection<RecipeHowToStep> Steps { get; } = steps;
    public string? Notes { get; } = notes;
}
