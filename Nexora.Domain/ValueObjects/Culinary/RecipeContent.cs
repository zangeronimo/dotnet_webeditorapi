namespace Nexora.Domain.ValueObjects.Culinary;

public class RecipeContent(
    string shortDescription,
    string fullDescription,
    IReadOnlyCollection<RecipeSection> sections,
    IReadOnlyCollection<string> notes)
{
    public string ShortDescription { get; } = shortDescription;
    public string FullDescription { get; } = fullDescription;

    public IReadOnlyCollection<RecipeSection> Sections { get; } = sections;
    public IReadOnlyCollection<string> Notes { get; } = notes;
}
