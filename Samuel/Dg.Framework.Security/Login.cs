using System.Diagnostics.Contracts;
using System.Net;
using Chabis.Functional;
using Microsoft.AspNetCore.Http;

namespace Dg.Framework.Security
{
    public static class Login
    {
        [Pure]
        public static Result<int, HttpStatusCode> CheckUserId(HttpContext context, int userId) =>
            (int)context.Session.GetInt32("userId") == userId
                ? (Result<int, HttpStatusCode>)userId
                : HttpStatusCode.Forbidden;
    }
}