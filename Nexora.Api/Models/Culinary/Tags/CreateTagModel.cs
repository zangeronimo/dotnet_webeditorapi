using System.ComponentModel.DataAnnotations;

using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Culinary.Tags;

public class CreateTagModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300, ErrorMessage = "O Descrição deve ter no máximo 300 caracteres.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }
}
