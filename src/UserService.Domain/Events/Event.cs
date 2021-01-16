using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class Event : IEvent<Guid>
    {
        public Event(Guid aggregatorId)
        {
            AggregatorId = aggregatorId;
            Timestamp = DateTime.UtcNow;
        }

        public Guid AggregatorId { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}