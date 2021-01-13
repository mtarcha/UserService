using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class CreateUserRequestEvent : IEvent<Guid>
    {
        public CreateUserRequestEvent(Guid userId, string email)
        {
            AggregatorId = userId;
            Timestamp = DateTime.UtcNow;
            Email = email;
        }

        public Guid AggregatorId { get; }
        public DateTime Timestamp { get; }
        public string Email { get; set; }
    }
}