using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dg.Core.Testing.TestAbstractions
{
    public class TestSession : ISession
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
}