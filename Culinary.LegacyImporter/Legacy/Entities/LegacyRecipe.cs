using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Culinary.LegacyImporter.Legacy.Entities;

public class LegacyRecipe
{
    public Guid Id { get; set; }
    public Slug Slug { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Preparation { get; set; }
    public Guid CategoryId { get; set; }
    public Status Active { get; set; }
    public Guid CompanyId { get; set; }
    public ICollection<LegacyRecipeImage> Images { get; set; } = [];
    public int Imported { get; set; }
}

