using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class Permission : Entity
{
    public string Code { get; private set; } = null!;
    public string Label { get; private set; } = null!;
    public Status Status { get; private set; }

    public Permission(string code, string label, Status status)
    {
        Code = code;
        Label = label;
        Status = status;
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
