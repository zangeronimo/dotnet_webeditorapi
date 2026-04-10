using WEBEditorAPI.Domain.Commands.Culinary;
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

    public void UpdateCategories(IEnumerable<UpdateCategoryCommand> commands)
    {
        var commandIds = commands
            .Where(c => c.Id != Guid.Empty)
            .Select(c => c.Id)
            .ToHashSet();

        // Soft delete
        foreach (var category in Categories)
        {
            if (!commandIds.Contains(category.Id))
            {
                category.Delete();
            }
        }

        // Add / Update
        foreach (var cmd in commands)
        {
            var existing = Categories.FirstOrDefault(c => c.Id == cmd.Id && c.DeletedAt == null);
            if (existing != null)
            {
                existing.Update(cmd.Slug, cmd.Name, cmd.Active);
            }
            else
            {
                var category = new Category(cmd.Slug, cmd.Name, cmd.Active, Id, CompanyId);
                _categories.Add(category);
            }
        }
    }
}
