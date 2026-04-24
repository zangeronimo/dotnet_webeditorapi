using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Culinary.Ratings;

public class UpdateRatingModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Rate é obrigatório.")]
    [Range(0, 5, ErrorMessage = "O valor de Rate deve estar entre 0 e 5.")]
    public int Rate { get; set; }

    public string? Name { get; set; }

    public string? Comment { get; set; }

    [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
    public Status Active { get; set; }

}
