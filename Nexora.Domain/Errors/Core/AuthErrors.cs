namespace Nexora.Domain.Errors.Core;

public static class AuthErrors
{
    public const string InvalidCredentials = "auth.invalid_credentials";
    public const string AccessDenied = "auth.access_denied";
    public const string TokenExpired = "auth.token_expired";
    public const string InvalidGrantType = "auth.invalid_grant_type";
    public const string CreateJwtError = "auth.create.jwt.error";
}
