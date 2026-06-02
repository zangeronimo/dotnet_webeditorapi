namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeSeo(
    string metaTitle,
    string metaDescription,
    string? canonicalUrl)
{
    public string MetaTitle { get; } = metaTitle;
    public string MetaDescription { get; } = metaDescription;
    public string? CanonicalUrl { get; } = canonicalUrl;
}