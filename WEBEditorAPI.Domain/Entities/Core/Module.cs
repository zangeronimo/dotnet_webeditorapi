using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class Module : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    private readonly List<Permission> _permissions = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;

    public Module(string name, Status status) : base()
    {
        Name = name;
        Status = status;
    }

    protected Module() : base() { }

    public void Update(string newName, Status newStatus)
    {
        Name = newName;
        Status = newStatus;
        Touch();
    }
}