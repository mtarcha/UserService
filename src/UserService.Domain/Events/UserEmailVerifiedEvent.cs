using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class UserEmailVerifiedEvent : IEvent<Guid>
    {
        public UserEmailVerifiedEvent(Guid aggregatorId)
        {
            AggregatorId = aggregatorId;
            Timestamp = DateTime.UtcNow;
        }

        public Guid AggregatorId { get; }
        public DateTime Timestamp { get; }
    }
}