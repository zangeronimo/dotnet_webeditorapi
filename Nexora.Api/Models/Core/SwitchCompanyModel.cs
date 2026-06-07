using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Models.Core;

public class SwitchCompanyModel
{
    [Required(ErrorMessage = "O campo CompanyId é obrigatório.")]
    public Guid CompanyId { get; set; }
}