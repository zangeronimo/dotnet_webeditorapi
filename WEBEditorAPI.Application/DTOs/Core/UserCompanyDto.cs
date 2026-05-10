using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Core;

public class UserCompanyDto : BaseDto
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string NickName { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public Status Status { get; set; }
    public DateTimeOffset LastAccessedAt { get; set; }
}
