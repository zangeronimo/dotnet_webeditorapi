using Microsoft.AspNetCore.Authorization;

namespace Nexora.Api.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
    {
        Policy = $"PERMISSION:{permission}";
    }
}
