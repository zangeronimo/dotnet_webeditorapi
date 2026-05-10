namespace WEBEditorAPI.Application.DTOs.Core;

public class UserProfileDto
{
    public UserDto User { get; set; } = null!;
    public UserCompanyDto UserCompany { get; set; } = null!;
}
