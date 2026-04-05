using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Domain.Entities.Culinary;

public class Category : Entity
{
    public Slug Slug { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public Guid CompanyId { get; private set; }
    public Status Active { get; private set; }
    public Guid LevelId { get; private set; }

    public Category(Slug slug, string name, Status active, Guid levelId, Guid companyId) : base()
    {
        Slug = slug;
        Name = name;
        Active = active;
        LevelId = levelId;
        CompanyId = companyId;
    }

    protected Category() : base() { }

    public void Update(Slug newSlug, string newName, Status newActive)
    {
        Slug = newSlug;
        Name = newName;
        Active = newActive;
        Touch();
    }
}
