namespace Culinary.LegacyImporter.Legacy.Entities;

public class LegacyRecipeImage
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public Guid RecipeId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public LegacyRecipe Recipe { get; set; } = null!;
}