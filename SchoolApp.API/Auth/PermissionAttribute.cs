using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.DAL.Models;

public class PermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public PermissionAttribute(string permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

        if (userManager == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var user = await userManager.GetUserAsync(context.HttpContext.User);
        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var hasPermission = user.GetType().GetProperty(_permission)?.GetValue(user) as bool?;
        if (hasPermission != true)
        {
            context.Result = new ForbidResult();
        }
    }
}
