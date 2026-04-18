namespace WEBEditorAPI.Domain.ValueObjects.Culinary;

public class RecipeMedia(string imageUrl)
{
    public string? ImageUrl { get; } = imageUrl;
}
