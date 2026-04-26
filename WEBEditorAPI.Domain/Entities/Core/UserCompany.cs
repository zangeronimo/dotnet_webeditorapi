using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class UserCompany : Entity
{
    public Guid UserId { get; protected set; }
    public User? User { get; protected set; }

    public Guid CompanyId { get; protected set; }
    public Company? Company { get; protected set; }

    public string? NickName { get; protected set; }
    public string? AvatarUrl { get; protected set; }
    public DateTimeOffset? InvitedAt { get; protected set; }
    public DateTimeOffset? JoinedAt { get; protected set; }

    public DateTimeOffset? LastAccessedAt { get; private set; }

    public Status Status { get; private set; }

    protected UserCompany() { }

    public UserCompany(Guid userId, Guid companyId, string? nickName, string? avatarUrl, Status status)
    {
        UserId = userId;
        CompanyId = companyId;
        NickName = nickName;
        AvatarUrl = avatarUrl;
        Status = status;
    }

    public void Update(string? newNickName, string? newAvatarUrl, Status newStatus)
    {
        NickName = newNickName;
        AvatarUrl = newAvatarUrl;
        Status = newStatus;
    }

    public void SetInvite()
    {
        InvitedAt = DateTimeOffset.Now;
    }

    public void SetJoined()
    {
        JoinedAt = DateTimeOffset.Now;
    }
}
