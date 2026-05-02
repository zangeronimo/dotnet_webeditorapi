using Microsoft.AspNetCore.Authorization;

namespace WEBEditorAPI.Api.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
    {
        Policy = $"PERMISSION:{permission}";
    }
}
