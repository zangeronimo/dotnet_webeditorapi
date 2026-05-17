using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Models.Core.Users;

public class UserProfileModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O E-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 8, ErrorMessage = "O Password deve ter entre 8 e 50 caracteres.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "O campo Apelido é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O Apelido deve ter no máximo 100 caracteres.")]
    public string NickName { get; set; } = string.Empty;
}

