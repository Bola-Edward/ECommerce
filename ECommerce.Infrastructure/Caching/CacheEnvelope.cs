using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Caching
{
    public sealed class CacheEnvelope<T>
    {
        public required T Payload { get; init; }

        public DateTime CreatedAtUtc { get; init; }

        public DateTime LastAccessedUtc { get; init; }
    }
}
