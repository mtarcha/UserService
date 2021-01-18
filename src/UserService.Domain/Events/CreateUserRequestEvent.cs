using System;

namespace UserService.Domain.Events
{
    public class CreateUserRequestEvent : Event
    {
        public CreateUserRequestEvent(Guid aggregateRootId, string email)
            : base(aggregateRootId)
        {
            Email = email;
        }

        public string Email { get; private set; }
    }
}