namespace WEBEditorAPI.Domain.Entities.Core;

public class CompanyModule
{
    public Guid CompanyId { get; set; }
    public Guid ModuleId { get; set; }

    public Company Company { get; set; } = null!;
    public Module Module { get; set; } = null!;
}
