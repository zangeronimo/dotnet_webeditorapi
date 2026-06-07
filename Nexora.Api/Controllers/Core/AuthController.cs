using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Nexora.Api.Models.Core;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Infrastructure.Options;

namespace Nexora.Api.Controllers.Core;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMakeLogin _login;
    private readonly IRefreshToken _refresh;
    private readonly ISwitchCompany _switchCompany;
    public AuthController(IMakeLogin login, IRefreshToken refresh, ISwitchCompany switchCompany)
    {
        _login = login;
        _refresh = refresh;
        _switchCompany = switchCompany;
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
            throw new ApiBadRequestException(AuthErrors.InvalidGrantType);

        Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(options.RefreshExpirationHours)
        });
        return Ok(result);
    }

    [Authorize]
    [HttpPost("switch-company")]
    public async Task<IActionResult> Authenticate([FromBody] SwitchCompanyModel model, [FromServices] IOptions<JwtOptions> jwtOptions)
    {
        var options = jwtOptions.Value;

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, model.CompanyId);
        var result = await _switchCompany.ExecuteAsync(context);

        Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(options.RefreshExpirationHours)
        });
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Append("refreshToken", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow
        });
        return NoContent();
    }

    private async Task<AuthResponse> MakeLogin(AuthRequest request)
    {
        return await _login.ExecuteAsync(request);
    }

    private async Task<AuthResponse> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            throw new ApiInvalidCredentialsException(AuthErrors.AccessDenied);
        }
        return await _refresh.ExecuteAsync(refreshToken);
    }
}

