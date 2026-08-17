using System.ComponentModel.DataAnnotations;

using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Culinary.RecipeRatings;

public class UpdateRecipeRatingModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Score é obrigatório.")]
    [Range(2, 10, ErrorMessage = "O Score deve ser entre 2 e 10")]
    public int Score { get; set; }

    [MaxLength(150, ErrorMessage = "O Name deve ter no máximo 150 caracteres.")]
    public string? Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "O Comment deve ter no máximo 500 caracteres.")]
    public string? Comment { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 3, ErrorMessage = "O Status deve ser entre 0 e 3")]
    public CulinaryRecipeRatingStatus Status { get; set; }

    [Required(ErrorMessage = "O campo RecipeId é obrigatório.")]
    public Guid RecipeId { get; set; }
}
