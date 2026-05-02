namespace WEBEditorAPI.Domain.ValueObjects.Culinary;

public record CategorySeo
{
    public string? MetaTitle { get; }
    public string? MetaDescription { get; }

    public CategorySeo(string? metaTitle, string? metaDescription)
    {
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
    }
}
