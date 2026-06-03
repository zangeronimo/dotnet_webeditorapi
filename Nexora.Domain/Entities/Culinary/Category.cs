using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Domain.Entities.Culinary;

public class Category : Entity
{
    public Slug Slug { get; private set; } = null!;
    public CategoryName Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Guid? ParentId { get; private set; }
    public Category? Parent { get; private set; }
    public ICollection<Category> Children { get; private set; } = new List<Category>();
    public int DisplayOrder { get; private set; }
    public Status Status { get; private set; }
    public CategorySeo Seo { get; private set; }
    public string? FeaturedImageUrl {get; private set;}
    public Guid CompanyId { get; private set; }

    public Category(
        Slug slug,
        CategoryName name,
        string? description,
        Guid? parentId,
        int displayOrder,
        Status status,
        CategorySeo seo,
        string? featuredImageUrl,
        Guid companyId) : base()
    {
        Slug = slug;
        Name = name;
        Description = description;
        ParentId = parentId;
        DisplayOrder = displayOrder;
        Status = status;
        Seo = seo;
        FeaturedImageUrl = featuredImageUrl;
        CompanyId = companyId;
    }

    protected Category() : base() { }

    public void Update(
        Slug newSlug,
        CategoryName newName,
        string? newDescription,
        Guid? newParentId,
        int newDisplayOrder,
        Status newStatus,
        CategorySeo newSeo,
        string? featuredImageUrl)
    {
        Slug = newSlug;
        Name = newName;
        Description = newDescription;
        ParentId = newParentId;
        DisplayOrder = newDisplayOrder;
        Status = newStatus;
        Seo = newSeo;
        FeaturedImageUrl = featuredImageUrl;
        Touch();
    }
}
