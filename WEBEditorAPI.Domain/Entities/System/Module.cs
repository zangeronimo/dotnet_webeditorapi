namespace WEBEditorAPI.Domain.Entities.System;

public class Module : Entity
{
    public string Name { get; private set; } = null!;

    public Module(string name) : base()
    {
        Name = name;
    }

    protected Module() : base() { }

    public void ChangeName(string newName)
    {
        Name = newName;
        Touch();
    }
}
