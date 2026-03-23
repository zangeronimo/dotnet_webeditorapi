using System.ComponentModel.DataAnnotations;

namespace WEBEditorAPI.Application.Models.System;

public class CreateUserModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O E-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Password é obrigatório.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "O Password deve ter entre 8 e 50 caracteres.")]
    public string Password { get; set; } = string.Empty;

    public Guid CompanyId { get; set; }
}
