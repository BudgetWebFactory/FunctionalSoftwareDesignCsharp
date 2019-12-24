using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
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

        private class TestSession : ISession
        {
            private IDictionary<string, byte[]> data = new Dictionary<string, byte[]>();

            public string Id => throw new NotImplementedException();

            public bool IsAvailable => throw new NotImplementedException();

            public IEnumerable<string> Keys => data.Keys;

            public void Clear() => data.Clear();

            public Task CommitAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task LoadAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public void Remove(string key) => data.Remove(key);

            public void Set(string key, byte[] value) => data[key] = value;

            public bool TryGetValue(string key, out byte[] value) => data.TryGetValue(key, out value);
        }

        private class TestHttpContext : HttpContext
        {
            private ISession session = new TestSession();

            public override ConnectionInfo Connection => throw new NotImplementedException();

            public override IFeatureCollection Features => throw new NotImplementedException();

            public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override HttpRequest Request => throw new NotImplementedException();

            public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override HttpResponse Response => throw new NotImplementedException();

            public override ISession Session { get => session; set => session = value; }
            public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override WebSocketManager WebSockets => throw new NotImplementedException();

            public override void Abort()
            {
                throw new NotImplementedException();
            }
        }
    }
}