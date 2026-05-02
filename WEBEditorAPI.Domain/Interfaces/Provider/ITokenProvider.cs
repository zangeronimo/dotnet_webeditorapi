using WEBEditorAPI.Domain.Security.System;

namespace WEBEditorAPI.Domain.Interfaces.Provider;

public enum TokenType
{
    Access,
    Refresh
}
public interface ITokenProvider
{
    public string GenerateToken(Guid userId, string username, IReadOnlyList<string> permissions, Guid companyId, TokenType type);
    public TokenPayload ValidateAccessToken(string token);
    public TokenPayload ValidateRefreshToken(string token);
}
