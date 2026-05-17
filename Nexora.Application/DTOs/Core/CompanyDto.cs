using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class CompanyDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public List<ModuleDto> Modules { get; set; } = new List<ModuleDto>();
}
