using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Models.Culinary.Recipes;

public class RecipeImageUploadModel
{
    [Required(ErrorMessage = "O campo Image é obrigatório.")]
    public IFormFile Image { get; set; } = default!;
}