using System.Text.RegularExpressions;

namespace WEBEditorAPI.Domain.ValueObjects;

public class Email
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; private set; }
    private Email(string value)
    {
        this.Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        if (!EmailRegex.IsMatch(email))
            throw new ArgumentException("Invalid Email.");

        return new Email(email);
    }

    public static Email Restore(string email)
    {
        return new Email(email);
    }

    public override string ToString() => Value;
}
