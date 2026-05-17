using System.ComponentModel.DataAnnotations;
using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Core.UserCompanies;

public class UpdateUserCompanyModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [MaxLength(100, ErrorMessage = "O Apelido deve ter no máximo 100 caracteres.")]
    public string? NickName { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [Range(0, 1, ErrorMessage = "O Status deve ser entre 0 e 1")]
    public Status Status { get; set; }
}
