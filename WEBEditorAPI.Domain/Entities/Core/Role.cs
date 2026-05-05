using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class Role : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    public Guid CompanyId { get; private set; }
    private readonly List<Permission> _permissions = [];
    public IReadOnlyCollection<Permission> Permissions => _permissions;

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

    public void SetPermissions(IEnumerable<Permission> permissions)
    {
        var newPermissions = permissions.ToList();
        _permissions.RemoveAll(m => !newPermissions.Any(n => n.Id == m.Id));
        foreach (var permission in newPermissions)
        {
            if (_permissions.All(m => m.Id != permission.Id))
            {
                _permissions.Add(permission);
            }
        }
    }

}
