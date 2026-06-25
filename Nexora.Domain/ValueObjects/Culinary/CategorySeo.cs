namespace Nexora.Domain.ValueObjects.Culinary;

public class CategorySeo(string? metaTitle, string? metaDescription)
{
    public string? MetaTitle { get; } = metaTitle;
    public string? MetaDescription { get; } = metaDescription;
}
