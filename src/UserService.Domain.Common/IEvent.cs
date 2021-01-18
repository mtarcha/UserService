using System;

namespace UserService.Domain.Common
{
    public interface IEvent<out TKey>
    {
        TKey AggregateRootId { get; }

        DateTime Timestamp { get; }
    }
}