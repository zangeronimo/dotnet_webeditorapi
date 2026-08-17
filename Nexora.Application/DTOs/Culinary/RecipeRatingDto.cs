using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Culinary;

public class RecipeRatingDto : BaseDto
{
    public int Score { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public Status Status { get; set; }
    public DateTimeOffset? PublishedAt { get; set; }
    public Guid RecipeId { get; set; }
}
