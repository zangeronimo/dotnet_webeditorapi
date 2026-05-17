using Nexora.Domain.Enums;

namespace Nexora.Application.DTOs.Core;

public class UserCompanyDto : BaseDto
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string? NickName { get; set; }
    public string? AvatarUrl { get; set; }
    public Status Status { get; set; }
    public DateTimeOffset LastAccessedAt { get; set; }
}
