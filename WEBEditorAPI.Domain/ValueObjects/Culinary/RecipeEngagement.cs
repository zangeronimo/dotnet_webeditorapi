namespace WEBEditorAPI.Domain.ValueObjects.Culinary;

public class RecipeEngagement(int views, int likes)
{
    public int Views { get; private set; } = views;
    public int Likes { get; private set; } = likes;
}
