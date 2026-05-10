using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Core.UserCompanies;

public class CreateUserCompanyModel
{
    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O E-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }
}
