using System.ComponentModel.DataAnnotations;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Models.Core.Modules;

public class UpdateModuleModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(45, ErrorMessage = "O Nome deve ter no máximo 45 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    public Status Status { get; set; }
    public List<PermissionDto> Permissions { get; set; } = [];
}
