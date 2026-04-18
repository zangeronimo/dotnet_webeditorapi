namespace WEBEditorAPI.Domain.ValueObjects.Culinary;

public class RecipeAttributes(string difficulty, string tools, string cuisine)
{
    public string Difficulty { get; } = difficulty;
    public string Tools { get; } = tools;
    public string Cuisine { get; } = cuisine;
}
