using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Dg.Core.Testing.TestAbstractions
{
    public class TestHttpContext : HttpContext
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