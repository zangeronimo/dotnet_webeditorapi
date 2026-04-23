namespace WEBEditorAPI.Infrastructure.Options;

public class JwtOptions
{
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpirationSeconds { get; set; }
    public int RefreshExpirationHours { get; set; }
}
