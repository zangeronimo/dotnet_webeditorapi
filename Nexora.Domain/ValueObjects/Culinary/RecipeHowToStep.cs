namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeHowToStep(int order, string instruction)
{
    public int Order { get; } = order;
    public string Instruction { get; } = instruction;
}
