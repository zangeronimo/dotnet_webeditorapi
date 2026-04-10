using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Domain.Commands.Culinary;

public class UpdateCategoryCommand
{
    public Guid Id { get; }
    public Slug Slug { get; }
    public string Name { get; }
    public Status Active { get; }

    public UpdateCategoryCommand(Guid id, Slug slug, string name, Status active)
    {
        Id = id;
        Slug = slug;
        Name = name;
        Active = active;
    }
}
