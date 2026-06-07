using Nexora.Domain.Enums;

namespace Culinary.LegacyImporter.Legacy.Entities;

public class LegacyRecipeRating
{
    public Guid Id { get; set; }
    public int Rate { get; set; }
    public string? Comment { get; set; }
    public string? Name { get; set; }
    public Status Active { get; set; }
    public Guid RecipeId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime? DeletedAt { get; set; }
}
