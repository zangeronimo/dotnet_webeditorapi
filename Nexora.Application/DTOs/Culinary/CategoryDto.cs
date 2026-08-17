using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Culinary;

public class CategoryDto : BaseDto
{
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public int DisplayOrder { get; set; }
    public Status Status { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? FeaturedImageUrl { get; set; }
}
