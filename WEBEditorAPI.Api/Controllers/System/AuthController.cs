using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Infrastructure.Options;

namespace WEBEditorAPI.Api.Controllers.System;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUseCase<AuthRequest, AuthResponse> Login;
    private readonly IUseCase<string, AuthResponse> Refresh;
    public AuthController(IUseCase<AuthRequest, AuthResponse> login, IUseCase<string, AuthResponse> refresh)
    {
        Login = login;
        Refresh = refresh;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] AuthRequest request, [FromServices] IOptions<JwtOptions> jwtOptions)
    {
        var options = jwtOptions.Value;

        AuthResponse result = null!;
        if (request.GrantType == "password")
            result = await MakeLogin(request);
        else if (request.GrantType == "refresh_token")
            result = await RefreshToken();
        else
            throw new ApiBadRequestException("Invalid grant_type");

        Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(options.RefreshExpirationHours)
        });
        return Ok(result);
    }

    private async Task<AuthResponse> MakeLogin(AuthRequest request)
    {
        return await Login.ExecuteAsync(request);
    }

    private async Task<AuthResponse> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            throw new ApiInvalidCredentialsException("Acesso negado");
        }
        return await Refresh.ExecuteAsync(refreshToken);
    }
}

