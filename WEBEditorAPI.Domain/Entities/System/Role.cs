namespace WEBEditorAPI.Domain.Entities.System;

public class Role : Entity
{
    public string Name { get; private set; } = null!;
    public string Label { get; private set; } = null!;
    public Guid ModuleId { get; private set; }

    public Role(string name, string label, Guid moduleId) : base()
    {
        Name = name;
        Label = label;
        ModuleId = moduleId;
    }

    protected Role() : base() { }

    public void Update(string newName, string newLabel)
    {
        Name = newName;
        Label = newLabel;
        Touch();
    }
}