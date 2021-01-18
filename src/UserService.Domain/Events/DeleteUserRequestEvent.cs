using System;

namespace UserService.Domain.Events
{
    public class DeleteUserRequestEvent : Event
    {
        public DeleteUserRequestEvent(Guid aggregateRootId)
            : base(aggregateRootId)
        {
        }
    }
}