using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Fit.Authorization
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string _role;
        public RoleAuthorizeAttribute(string role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            var UserId = user.FindFirstValue("uid");

            if (UserId is null)
            {
                context.Result = new ForbidResult($"Access denied. You must be a Login.");
                return;
            }
            if(!user.IsInRole(_role))
            {
                context.Result = new ForbidResult($"Access denied. You must be a {_role}.");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
