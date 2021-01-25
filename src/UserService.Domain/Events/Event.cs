using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class Event : IEvent<Guid>
    {
        public Event(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
            Timestamp = DateTime.UtcNow;
        }

        public Guid AggregateRootId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
    }
}