using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Application.DTOs.System;

public class UserResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid CompanyId { get; set; }
    public UserResponse(User user)
    {
        Name = user.Name;
        Email = user.Email.Value;
        CompanyId = user.CompanyId;
    }
}
