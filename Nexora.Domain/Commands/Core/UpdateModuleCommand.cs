using Nexora.Domain.Enums;

namespace Nexora.Domain.Commands.Core;

public class UpdateModuleCommand
{
    public Guid Id { get; }
    public string Name { get; }
    public Status Status { get; }

    public UpdateModuleCommand(Guid id, string name, Status status)
    {
        Id = id;
        Name = name;
        Status = status;
    }
}
