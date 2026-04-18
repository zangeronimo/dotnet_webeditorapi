namespace WEBEditorAPI.Domain.ValueObjects;

public class RecipeYield(string yieldTotal)
{
    public string YieldTotal { get; } = yieldTotal;
}
