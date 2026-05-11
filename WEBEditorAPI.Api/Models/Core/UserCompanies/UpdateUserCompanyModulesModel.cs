using System.ComponentModel.DataAnnotations;

namespace WEBEditorAPI.Api.Models.Core.UserCompanies;

public class UpdateUserCompanyModulesModel
{
    [Required(ErrorMessage = "O campo Roles é obrigatório.")]
    public List<ModuleRoleModel> Roles { get; set; } = new List<ModuleRoleModel>();
}
