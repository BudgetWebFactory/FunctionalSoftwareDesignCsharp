using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Dg.Core.Testing.TestAbstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using NUnit.Framework;
using static Dg.Framework.Security.Login;

namespace Dg.Framework.Security
{
    [TestFixture]
    public class CheckUserIdTests
    {
        private const int userId = 1;
        private const int invalidUserId = 2;
        private static HttpContext contextWithoutUserId = new TestHttpContext();

        private static HttpContext CreateContextWithUserId()
        {
            var context = new TestHttpContext();
            context.Session.SetInt32("userId", userId);
            return context;
        }
        private static HttpContext contextWithUserId = CreateContextWithUserId();

        [Test]
        public void NoUserIdInContext_ReturnsErrorUnauthorized() =>
            Assert.That(() => CheckUserId(contextWithoutUserId, userId).Match(onOk: _ => HttpStatusCode.OK, onErr: e => e), Is.EqualTo(HttpStatusCode.Unauthorized));

        [Test]
        public void MismatchInUserIds_ReturnsErrorUnauthorized() =>
            Assert.That(() => CheckUserId(contextWithUserId, invalidUserId).Match(onOk: _ => HttpStatusCode.OK, onErr: e => e), Is.EqualTo(HttpStatusCode.Unauthorized));


        [Test]
        public void MatchingUserIds_ReturnsOkUserId() =>
            Assert.That(() => CheckUserId(contextWithUserId, userId).Match(onOk: e => e, onErr: _ => -1), Is.EqualTo(userId));
    }
}