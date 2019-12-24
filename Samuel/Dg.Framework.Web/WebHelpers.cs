using System.Diagnostics.Contracts;
using System.Net;
using Chabis.Functional;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dg.Framework.Web
{
    public static class WebHelpers
    {
        [Pure]
        public static Result<bool, HttpStatusCode> ValidateModelState(ModelStateDictionary modelState) =>
            modelState.IsValid
                ? (Result<bool, HttpStatusCode>)true
                : HttpStatusCode.BadRequest;
    }
}