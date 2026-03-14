namespace WEBEditorAPI.Domain.Entities.System;

public class Module : Entity
{
    public string Name { get; private set; } = null!;
    public ICollection<Role> Roles { get; set; } = new List<Role>();

    public Module(string name) : base()
    {
        Name = name;
    }

    protected Module() : base() { }

    public void Update(string newName)
    {
        Name = newName;
        Touch();
    }
}
