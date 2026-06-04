using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Exceptions;

namespace Nexora.Domain.ValueObjects.Culinary;

public record CategoryName
{
    public string Value { get; }
    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(CategoryErrors.NameRequired);
        Value = value.Trim();
    }
    public override string ToString() => Value;
}
