using System.ComponentModel.DataAnnotations;
using Nexora.Domain.Enums;

namespace Nexora.Api.Models.System.Users;

public class UpdateUserModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O E-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 8, ErrorMessage = "O Password deve ter entre 8 e 50 caracteres.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }
}
