using WEBEditorAPI.Domain.Security.System;

namespace WEBEditorAPI.Domain.Interfaces.Provider;

public enum TokenType
{
    Access,
    Refresh
}
public interface ITokenProvider
{
    public string GenerateToken(Guid userId, string username, Guid companyId, TokenType type);
    public TokenPayload ValidateToken(string token);
}
