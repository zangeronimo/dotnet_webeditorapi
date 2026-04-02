using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Application.Models.System;

public class UpdateUserModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid? Id { get; set; }
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O E-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 8, ErrorMessage = "O Password deve ter entre 8 e 50 caracteres.")]
    public string? Password { get; set; }
    public List<Role> Roles { get; set; } = [];
    public Guid CompanyId { get; set; }
}
