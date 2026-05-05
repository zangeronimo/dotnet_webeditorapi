using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Core.Roles;

public class CreateRoleModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(20, ErrorMessage = "O Nome deve ter no máximo 20 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }
}
