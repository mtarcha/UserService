using System;
using MediatR;

namespace UserService.Domain.Common
{
    public interface IEvent<out TKey> : INotification
    {
        TKey AggregateRootId { get; }

        DateTimeOffset Timestamp { get; }
    }
}