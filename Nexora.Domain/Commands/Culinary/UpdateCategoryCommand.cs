using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Nexora.Domain.Commands.Culinary;

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
