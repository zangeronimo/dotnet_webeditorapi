using System.ComponentModel.DataAnnotations;
using Nexora.Domain.Enums;

namespace Nexora.Api.Models.System.Roles;

public class UpdateRoleModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(20, ErrorMessage = "O Nome deve ter no máximo 20 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    public Status Status { get; set; }
}
