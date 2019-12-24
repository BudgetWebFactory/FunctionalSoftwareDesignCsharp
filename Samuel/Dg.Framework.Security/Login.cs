using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using Chabis.Functional;
using Microsoft.AspNetCore.Http;

namespace Dg.Framework.Security
{
    public static class Login
    {
        [Pure]
        public static Result<int, HttpStatusCode> CheckUserId(HttpContext context, int userId) =>
            context.Session.Keys.Any(e => e == "userId") && (int)context.Session.GetInt32("userId") == userId
                ? (Result<int, HttpStatusCode>)userId
                : HttpStatusCode.Unauthorized;
    }
}