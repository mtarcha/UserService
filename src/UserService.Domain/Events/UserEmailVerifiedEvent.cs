using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class UserEmailVerifiedEvent : Event
    {
        public UserEmailVerifiedEvent(Guid aggregatorId)
            : base(aggregatorId)
        {
        }
    }
}