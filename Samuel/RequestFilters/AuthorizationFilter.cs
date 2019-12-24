using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dg.Framework.Security
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Session.SetInt32("userId", GetUserId(context));
        }

        private static int GetUserId(AuthorizationFilterContext context) =>
            context.HttpContext.Request.Query.ContainsKey("uId")
                ? int.Parse(context.HttpContext.Request.Query["uId"])
                : 1;
    }
}