namespace WEBEditorAPI.Application.Requests;

public sealed record RequestContext(
    Guid UserId,
    Guid CompanyId,
    Guid RequestId = default
)
{
    public Guid RequestId { get; init; } = RequestId == default ? Guid.NewGuid() : RequestId;
}