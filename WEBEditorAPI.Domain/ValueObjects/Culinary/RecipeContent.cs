namespace WEBEditorAPI.Domain.ValueObjects.Culinary;

public class RecipeContent(
    string shortDescription,
    string fullDescription,
    string ingredients,
    string preparation,
    string notes)
{
    public string ShortDescription { get; } = shortDescription;
    public string FullDescription { get; } = fullDescription;
    public string Ingredients { get; } = ingredients;
    public string Preparation { get; } = preparation;
    public string? Notes { get; } = notes;
}
