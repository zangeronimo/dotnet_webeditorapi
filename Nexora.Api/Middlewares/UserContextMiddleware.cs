using System.Security.Claims;

namespace Nexora.Api.Middlewares;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var uId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cId = context.User.FindFirst("companyId")?.Value;

            if (Guid.TryParse(uId, out var userId) && Guid.TryParse(cId, out var companyId))
            {
                context.Items["CompanyId"] = companyId;
                context.Items["UserId"] = userId;
            }
        }
        await _next(context);
    }
}