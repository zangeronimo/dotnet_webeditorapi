using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Culinary.Levels;

public class UpdateLevelModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Slug é obrigatório.")]
    [MaxLength(45, ErrorMessage = "O Slug deve ter no máximo 45 caracteres.")]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(45, ErrorMessage = "O Nome deve ter no máximo 45 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
    public Status Active { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
}
