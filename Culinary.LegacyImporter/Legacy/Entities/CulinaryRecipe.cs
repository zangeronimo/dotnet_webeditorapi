using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Nexora.Domain.Entities.Culinary;

public class CulinaryRecipe
{
    public Guid Id { get; set; }
    public Slug Slug { get; set; }
    public string Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public string Ingredients { get; set; }
    public string Preparation { get; set; }
    public string? YieldTotal { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int RestTime { get; set; }
    public string? Difficulty { get; set; }
    public string? Notes { get; set; }
    public string? Cuisine { get; set; }
    public string? ImageUrl { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid CategoryId { get; set; }
    public Guid CompanyId { get; set; }
    public Status Active { get; set; }
}
