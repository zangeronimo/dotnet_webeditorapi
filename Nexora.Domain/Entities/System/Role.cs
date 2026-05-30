using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Entities.System;

public class Role : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    public Guid CompanyId { get; private set; }
    private readonly List<RolePermission> _rolePermissions = [];
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;

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
        var newPermissionIds = permissions.Select(m => m.Id).ToHashSet();
        _rolePermissions.RemoveAll(m => !newPermissionIds.Contains(m.PermissionId));
        foreach (var permission in permissions)
        {
            if (_rolePermissions.All(m => m.PermissionId != permission.Id))
            {
                var rolePermission = new RolePermission() { PermissionId = permission.Id, RoleId = Id, Role = this, Permission = permission };
                _rolePermissions.Add(rolePermission);
            }
        }
    }

}
