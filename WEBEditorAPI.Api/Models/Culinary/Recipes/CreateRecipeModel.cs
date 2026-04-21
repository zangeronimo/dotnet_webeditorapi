using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Culinary.Recipes;

public class CreateRecipeModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(120, ErrorMessage = "O Nome deve ter no máximo 120 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Descrição Curta é obrigatório.")]
    [MaxLength(255, ErrorMessage = "A Descrição Curta deve ter no máximo 255 caracteres.")]
    public string ShortDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Descrição Longa é obrigatório.")]
    public string FullDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Ingredientes é obrigatório.")]
    public string Ingredients { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Preparação é obrigatório.")]
    public string Preparation { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Rendimento é obrigatório.")]
    [MaxLength(50, ErrorMessage = "A Rendimento deve ter no máximo 50 caracteres.")]
    public string YieldTotal { get; set; } = string.Empty;

    public int PrepTime { get; set; } = 0;

    public int CookTime { get; set; } = 0;

    public int RestTime { get; set; } = 0;

    [Required(ErrorMessage = "O campo Dificuldade é obrigatório.")]
    [MaxLength(20, ErrorMessage = "A Dificuldade deve ter no máximo 20 caracteres.")]
    public string Difficulty { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Utensílios é obrigatório.")]
    public string Tools { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Cozinha é obrigatório.")]
    [MaxLength(100, ErrorMessage = "A Cozinha deve ter no máximo 100 caracteres.")]
    public string Cuisine { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Notas do Chefe é obrigatório.")]
    public string Notes { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Meta Título é obrigatório.")]
    [MaxLength(255, ErrorMessage = "A Meta Título deve ter no máximo 255 caracteres.")]
    public string MetaTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Meta Descrição é obrigatório.")]
    [MaxLength(255, ErrorMessage = "A Meta Descrição deve ter no máximo 255 caracteres.")]
    public string MetaDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Palavras Chave é obrigatório.")]
    public string Keywords { get; set; } = string.Empty;

    [Range(typeof(Guid), "00000000-0000-0000-0000-000000000001", "ffffffff-ffff-ffff-ffff-ffffffffffff",
    ErrorMessage = "O campo Guid do Level é obrigatório.")]
    public Guid LevelId { get; set; }
}