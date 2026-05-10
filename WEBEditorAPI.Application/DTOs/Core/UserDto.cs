namespace WEBEditorAPI.Application.DTOs.Core;

public class UserDto : BaseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
