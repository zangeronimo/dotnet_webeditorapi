namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeYield(string yieldTotal)
{
    public string YieldTotal { get; } = yieldTotal;
}
