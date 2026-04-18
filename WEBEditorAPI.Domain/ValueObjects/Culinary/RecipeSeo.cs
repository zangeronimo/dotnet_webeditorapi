namespace WEBEditorAPI.Domain.ValueObjects.Culinary;

public class RecipeSeo(
    string metaTitle,
    string metaDescription,
    List<string> keywords)
{
    public string MetaTitle { get; } = metaTitle;
    public string MetaDescription { get; } = metaDescription;
    public List<string> Keywords { get; } = keywords;
}