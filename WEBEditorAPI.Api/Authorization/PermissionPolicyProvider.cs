using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WEBEditorAPI.Api.Authorization;

public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options) { }

    public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("PERMISSION:"))
        {
            var permission = policyName.Split(':')[1];

            var policy = new AuthorizationPolicyBuilder()
                .RequireClaim("permission", permission)
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }

        return base.GetPolicyAsync(policyName);
    }
}
