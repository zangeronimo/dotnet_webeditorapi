using System.ComponentModel.DataAnnotations;

namespace WEBEditorAPI.Api.Models;

public class PaginationModel
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Page deve ser maior que 0.")]
    public int Page { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "PageSize deve estar entre 1 e 100.")]
    public int PageSize { get; set; }
}