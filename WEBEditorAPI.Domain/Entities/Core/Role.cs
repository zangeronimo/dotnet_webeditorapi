using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class Role : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    public Guid CompanyId { get; private set; }

    public ICollection<Permission> Permissions { get; private set; } = new List<Permission>();

    public Role(string name, Status status, Guid companyId) : base()
    {
        Name = name;
        Status = status;
        CompanyId = companyId;
    }

    protected Role() : base() { }

    public void Update(string newName, Status newStatus)
    {
        Name = newName;
        Status = newStatus;
        Touch();
    }
}
