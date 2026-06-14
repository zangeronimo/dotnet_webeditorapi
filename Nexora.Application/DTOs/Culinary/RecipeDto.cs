using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Application.DTOs.Culinary;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string FullDescription { get; set; } = string.Empty;
    public IReadOnlyCollection<RecipeSection> Sections { get; set; } = [];
    public IReadOnlyCollection<string> Notes { get; set; } = [];
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int RestTime { get; set; }
    public string YieldTotal { get; set; } = string.Empty;
    public CulinaryRecipeDifficulty Difficulty { get; set; }
    public string Cuisine { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string? CanonicalUrl { get; set; }
    public IReadOnlyCollection<Guid> TagIds { get; set; } = [];
    public Status Status { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalRatings { get; set; }
    public DateTimeOffset? PublishedAt { get; set; }
    public Guid CategoryId { get; set; }
}
