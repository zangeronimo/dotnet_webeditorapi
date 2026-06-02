namespace Nexora.Domain.ValueObjects.Culinary;

public record CategorySeo
{
    public string? MetaTitle { get; }
    public string? MetaDescription { get; }
    public string? CanonicalUrl { get; }

    public CategorySeo(string? metaTitle, string? metaDescription, string? canonicalUrl)
    {
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        CanonicalUrl = canonicalUrl;
    }
}
