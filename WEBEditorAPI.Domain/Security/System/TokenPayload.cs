namespace WEBEditorAPI.Domain.Security.System;

public class TokenPayload
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public List<string> Permissions { get; set; } = new();

    public TokenPayload(Guid userId, Guid companyId, List<string>? permissions = null)
    {
        UserId = userId;
        CompanyId = companyId;
        Permissions = permissions ?? new List<string>();
    }
}
