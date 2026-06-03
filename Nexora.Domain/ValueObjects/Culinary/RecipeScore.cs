using Nexora.Domain.Errors.Culiarny;
using Nexora.Domain.Exceptions;

namespace Nexora.Domain.ValueObjects.Culinary;

public sealed class RecipeScore
{
    public int Value { get; }

    public RecipeScore(int value)
    {
        if (value < 2 || value > 10)
            throw new DomainException(RecipeErrors.InvalidScore);

        Value = value;
    }
}