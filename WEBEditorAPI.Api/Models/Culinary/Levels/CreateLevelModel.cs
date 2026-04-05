using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Culinary.Levels;

public class CreateLevelModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo 45 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
    public Status Active { get; set; }

}
