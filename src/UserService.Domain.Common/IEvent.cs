using System;

namespace UserService.Domain.Common
{
    public interface IEvent<out TKey>
    {
        TKey AggregateRootId { get; }

        DateTimeOffset Timestamp { get; }
    }
}