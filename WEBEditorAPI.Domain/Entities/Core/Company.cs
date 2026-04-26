using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class Company : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    public ICollection<UserCompany> Users { get; set; } = [];

    public Company(string name, Status status) : base()
    {
        Name = name;
        Status = status;
    }

    protected Company() : base() { }

    public void Update(string newName, Status newStatus)
    {
        Name = newName;
        Status = newStatus;
        Touch();
    }
}
