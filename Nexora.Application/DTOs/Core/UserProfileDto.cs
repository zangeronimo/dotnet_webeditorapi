using Nexora.Application.DTOs.Core;

namespace Nexora.Application.DTOs.System;

public class UserProfileDto
{
    public UserCompanyDto UserCompany { get; set; } = null!;
    public List<CompanyDto> Companies { get; set; } = [];
}
