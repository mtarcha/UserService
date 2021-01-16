using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class DeleteUserRequestEvent : Event
    {
        public DeleteUserRequestEvent(Guid aggregatorId)
            : base(aggregatorId)
        {
        }
    }
}