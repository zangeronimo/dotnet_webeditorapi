using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Models.AI;

public class GenerateContentModel
{
    [Required(ErrorMessage = "O campo Prompt é obrigatório.")]
    [MaxLength(50000, ErrorMessage = "O Prompt deve ter no máximo 50000 caracteres.")]
    public string Prompt { get; set; } = string.Empty;

}
