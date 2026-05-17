using Nexora.Domain.Commands.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Exceptions;

namespace Nexora.Domain.Entities.Core;

public class Module : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    private readonly List<Permission> _permissions = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;
    public ICollection<CompanyModule> CompanyModules { get; set; } = new List<CompanyModule>();

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

    public void UpdatePermissions(IEnumerable<UpdatePermissionCommand> commands)
    {
        var commandIds = commands
            .Where(c => c.Id != Guid.Empty)
            .Select(c => c.Id)
            .ToHashSet();

        // Soft delete
        foreach (var permission in Permissions)
        {
            if (!commandIds.Contains(permission.Id))
            {
                permission.Delete();
            }
        }

        // Add / Update
        foreach (var cmd in commands)
        {
            if (cmd.Id != Guid.Empty)
            {
                var existing = Permissions.FirstOrDefault(c => c.Id == cmd.Id);
                if (existing == null)
                {
                    throw new DomainException($"Permission {cmd.Label} não pertence ao Modulo {Name}");
                }
                existing.Update(cmd.Code, cmd.Label, cmd.Status);
                continue;
            }

            var permission = new Permission(cmd.Code, cmd.Label, cmd.Status, Id);
            _permissions.Add(permission);
        }
    }
}