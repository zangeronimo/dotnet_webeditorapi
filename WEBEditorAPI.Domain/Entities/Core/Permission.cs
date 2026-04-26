using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class Permission : Entity
{
    public string Code { get; private set; } = null!;
    public string Label { get; private set; } = null!;
    public Status Status { get; private set; }
    public Guid ModuleId { get; private set; }

    public Permission(string code, string label, Status status, Guid moduleId)
    {
        Code = code;
        Label = label;
        Status = status;
        ModuleId = moduleId;
    }

    protected Permission() : base() { }

    public void Update(string newCode, string newLabel, Status newStatus)
    {
        Code = newCode;
        Label = newLabel;
        Status = newStatus;
        Touch();
    }
}
