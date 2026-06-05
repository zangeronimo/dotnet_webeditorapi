using System.ComponentModel.DataAnnotations;

using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Api.Models.Culinary.Recipes;

public class CreateRecipeModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo ShortDescription é obrigatório.")]
    [MaxLength(255, ErrorMessage = "O Descrição deve ter no máximo 255 caracteres.")]
    public string ShortDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo FullDescription é obrigatório.")]
    [MaxLength(500, ErrorMessage = "O Descrição deve ter no máximo 500 caracteres.")]
    public string FullDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Ingredients é obrigatório.")]
    public List<RecipeIngredient> Ingredients { get; set; } = [];

    [Required(ErrorMessage = "O campo Steps é obrigatório.")]
    public List<RecipeHowToStep> Steps { get; set; } = [];

    public string? Notes { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int RestTime { get; set; }

    [Required(ErrorMessage = "O campo YieldTotal é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O YieldTotal deve ter no máximo 50 caracteres.")]
    public string YieldTotal { get; set; } = string.Empty;

    public CulinaryRecipeDifficulty difficulty { get; set; }

    [Required(ErrorMessage = "O campo Cuisine é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O Cuisine deve ter no máximo 100 caracteres.")]
    public string Cuisine { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo MetaTitle é obrigatório.")]
    [MaxLength(70, ErrorMessage = "A MetaTitle deve ter no máximo 70 caracteres.")]
    public string MetaTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo MetaDescription é obrigatório.")]
    [MaxLength(170, ErrorMessage = "A MetaDescription deve ter no máximo 170 caracteres.")]
    public string MetaDescription { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "A CanonicalUrl deve ter no máximo 500 caracteres.")]
    public string? CanonicalUrl { get; set; }

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }

    [Required(ErrorMessage = "O campo CategoryId é obrigatório.")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "O campo TagIds é obrigatório.")]
    public List<Guid> TagIds { get; set; } = [];
}
