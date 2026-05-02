using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WEBEditorAPI.Domain.Security.System;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Infrastructure.Options;
using WEBEditorAPI.Application.Exceptions;

namespace WEBEditorAPI.Infrastructure.Provider;

public class JwtProvider : ITokenProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(Guid userId, string username, IReadOnlyList<string> permissions, Guid companyId, TokenType type)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim("companyId", companyId.ToString()),
            new Claim("token_type", type.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        if (type == TokenType.Access && permissions != null)
        {
            claims.AddRange(permissions.Select(p => new Claim("permission", p)));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        TimeSpan lifetime = type switch
        {
            TokenType.Access => TimeSpan.FromSeconds(_options.ExpirationSeconds),
            TokenType.Refresh => TimeSpan.FromHours(_options.RefreshExpirationHours),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(lifetime),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ClaimsPrincipal Validate(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_options.Secret);

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _options.Issuer,
            ValidateAudience = true,
            ValidAudience = _options.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };

        return tokenHandler.ValidateToken(token, parameters, out _);
    }

    private TokenPayload BuildPayload(ClaimsPrincipal principal)
    {
        var permissions = principal.FindAll("permission").Select(c => c.Value).ToList();
        return new TokenPayload(Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)!.Value), Guid.Parse(principal.FindFirst("companyId")!.Value), permissions);
    }

    public TokenPayload ValidateAccessToken(string token)
    {
        var principal = Validate(token);
        var type = principal.FindFirst("token_type")?.Value;
        if (type != TokenType.Access.ToString())
            throw new ApiInvalidCredentialsException("Invalid token type");
        return BuildPayload(principal);
    }

    public TokenPayload ValidateRefreshToken(string token)
    {
        var principal = Validate(token);
        var type = principal.FindFirst("token_type")?.Value;
        if (type != TokenType.Refresh.ToString())
            throw new ApiInvalidCredentialsException("Invalid token type");
        return BuildPayload(principal);
    }
}
