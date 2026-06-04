using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Nexora.Domain.Entities.Culinary;

public class Tag : Entity
{
    public Slug Slug { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Status Status { get; private set; }
    public Guid CompanyId { get; private set; }

    public Tag(
     Slug slug,
     string name,
     string? description,
     Status status,
     Guid companyId) : base()
    {
        Slug = slug;
        Name = name;
        Description = description;
        Status = status;
        CompanyId = companyId;
    }

    protected Tag() : base() { }

    public void Update(
        Slug newSlug,
        string newName,
        string? newDescription,
        Status newStatus)
    {
        Slug = newSlug;
        Name = newName;
        Description = newDescription;
        Status = newStatus;
        Touch();
    }
}