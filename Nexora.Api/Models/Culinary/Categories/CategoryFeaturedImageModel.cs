using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Models.Culinary.Categories;

public class CategoryFeaturedImageModel
{
    [Required(ErrorMessage = "O campo FeaturedImage é obrigatório.")]
    public IFormFile FeaturedImage { get; set; } = default!;
}
