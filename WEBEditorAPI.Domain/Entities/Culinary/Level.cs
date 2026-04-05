using System;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Domain.Entities.Culinary;

public class Level : Entity
{
    public Slug Slug { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public Guid CompanyId { get; private set; }
    public Status Active { get; private set; }
    private readonly List<Category> _categories = [];
    public IReadOnlyCollection<Category> Categories => _categories;

    public Level(Slug slug, string name, Status active, Guid companyId) : base()
    {
        Slug = slug;
        Name = name;
        Active = active;
        CompanyId = companyId;
    }

    protected Level() : base() { }

    public void Update(Slug newSlug, string newName, Status newActive)
    {
        Slug = newSlug;
        Name = newName;
        Active = newActive;
        Touch();
    }

    public void UpdateCategories(IEnumerable<Category> categories)
    {
        _categories.Clear();
        _categories.AddRange(categories);

    }
}
