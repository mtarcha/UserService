using System;

namespace UserService.Domain.Events
{
    public class UserEmailVerifiedEvent : Event
    {
        public UserEmailVerifiedEvent(Guid aggregateRootId)
            : base(aggregateRootId)
        {
        }
    }
}