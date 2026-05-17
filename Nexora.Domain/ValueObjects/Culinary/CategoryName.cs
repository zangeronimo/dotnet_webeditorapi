namespace Nexora.Domain.ValueObjects.Culinary;

public record CategoryName
{
    public string Value { get; }
    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Nome da categoria é obrigatório");
        Value = value.Trim();
    }
    public override string ToString() => Value;
}
