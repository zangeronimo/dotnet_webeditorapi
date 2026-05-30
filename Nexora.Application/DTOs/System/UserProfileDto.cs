using Nexora.Application.DTOs.Core;

namespace Nexora.Application.DTOs.System;

public class UserProfileDto
{
    public UserDto User { get; set; } = null!;
    public UserCompanyDto UserCompany { get; set; } = null!;
}
