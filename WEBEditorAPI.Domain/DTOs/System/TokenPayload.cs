namespace WEBEditorAPI.Domain.DTOs.System;

public class TokenPayload
{
    public Guid UserId;
    public Guid CompanyId;

    public TokenPayload(Guid userId, Guid companyId)
    {
        UserId = userId;
        CompanyId = companyId;
    }
}
