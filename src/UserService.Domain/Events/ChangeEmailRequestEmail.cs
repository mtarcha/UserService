using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class ChangeEmailRequestEmail : IEvent<Guid>
    {
        public ChangeEmailRequestEmail(Guid aggregatorId, string newEmail)
        {
            AggregatorId = aggregatorId;
            NewEmail = newEmail;
            Timestamp = DateTime.UtcNow;
        }

        public Guid AggregatorId { get; }
        public DateTime Timestamp { get; }
        public string NewEmail { get; set; }
    }
}