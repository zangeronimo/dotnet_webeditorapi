using System.ComponentModel.DataAnnotations;

using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Culinary.Categories;

public class CreateCategoryModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O Nome deve ter no máximo 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "O Descrição deve ter no máximo 500 caracteres.")]
    public string Description { get; set; } = string.Empty;

    public Guid? ParentId { get; set; }

    [Required(ErrorMessage = "O campo Ordem de exibição é obrigatório.")]
    public int DisplayOrder { get; set; }

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }

    [MaxLength(70, ErrorMessage = "A MetaTitle deve ter no máximo 70 caracteres.")]
    public string? MetaTitle { get; set; }

    [MaxLength(170, ErrorMessage = "A MetaDescription deve ter no máximo 170 caracteres.")]
    public string? MetaDescription { get; set; }
}
