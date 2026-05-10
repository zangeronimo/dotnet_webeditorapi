using System.ComponentModel.DataAnnotations;

namespace WEBEditorAPI.Api.Models.Core.Companies;

public class CompanyProfileModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O Nome deve ter no máximo 200 caracteres.")]
    public string Name { get; set; } = string.Empty;
}
