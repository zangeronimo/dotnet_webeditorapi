using System;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Commands.Core;

public class UpdatePermissionCommand
{
    public Guid Id { get; }
    public string Code { get; }
    public string Label { get; }
    public Status Status { get; }

    public UpdatePermissionCommand(Guid id, string code, string label, Status status)
    {
        Id = id;
        Code = code;
        Label = label;
        Status = status;
    }
}
