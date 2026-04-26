namespace WEBEditorAPI.Domain.Entities.Core;

public class UserCompanyModuleRole
{
    public Guid UserCompanyId { get; private set; }
    public Guid ModuleId { get; private set; }
    public Guid RoleId { get; private set; }

    protected UserCompanyModuleRole() { }

    public UserCompanyModuleRole(Guid userCompanyId, Guid moduleId, Guid roleId)
    {
        UserCompanyId = userCompanyId;
        ModuleId = moduleId;
        RoleId = roleId;
    }
}
