using System.ComponentModel.DataAnnotations;
using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Core.Modules;

public class CreateModuleModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(30, ErrorMessage = "O Nome deve ter no máximo 30 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }
}
