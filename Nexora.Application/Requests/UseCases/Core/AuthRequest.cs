using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexora.Application.Requests.UseCases.Core;

public class AuthRequest : IValidatableObject
{
    public string? Email { get; set; } = null!;
    public string? Password { get; set; } = null!;

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (GrantType == "password")
        {
            if (string.IsNullOrWhiteSpace(Email))
                yield return new ValidationResult("Email é obrigatório", new[] { nameof(Email) });

            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult("Password é obrigatório", new[] { nameof(Password) });
        }
    }
}
