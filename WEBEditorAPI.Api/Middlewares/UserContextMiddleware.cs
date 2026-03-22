using System.Security.Claims;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Api.Middlewares;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var uId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cId = context.User.FindFirst("companyId")?.Value;

            if (Guid.TryParse(uId, out var userId) && Guid.TryParse(cId, out var companyId))
            {
                User? user = await userRepository.GetByIdAsync(userId, companyId);
                if (user != null && user.CompanyId == companyId)
                {
                    var identity = (ClaimsIdentity)context.User.Identity;
                    foreach (var role in user.Roles)
                    {
                        if (!identity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == role.Name))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                        }
                    }
                    context.Items["CompanyId"] = companyId;
                }
            }
        }
        await _next(context);
    }
}