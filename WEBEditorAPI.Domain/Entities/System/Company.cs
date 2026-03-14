namespace WEBEditorAPI.Domain.Entities.System;


public class Company : Entity
{
    public string Name { get; private set; } = null!;
    public ICollection<Module> Modules { get; set; } = new List<Module>();

    public Company(string name) : base()
    {
        Name = name;
    }

    protected Company() : base() { }

    public void ChangeName(string newName)
    {
        Name = newName;
        Touch();
    }
}
