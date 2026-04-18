namespace WEBEditorAPI.Domain.ValueObjects.Culinary;

public class RecipeTiming(int prepTime, int cookTime, int restTime)
{
    public int PrepTime { get; } = prepTime;
    public int CookTime { get; } = cookTime;
    public int RestTime { get; } = restTime;
}
