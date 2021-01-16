using System;

namespace UserService.Domain.Common
{
    public interface IEvent<out TKey>
    {
        TKey AggregatorId { get; }

        DateTime Timestamp { get; }
    }
}