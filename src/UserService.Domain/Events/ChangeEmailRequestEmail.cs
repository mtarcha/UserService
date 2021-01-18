using System;

namespace UserService.Domain.Events
{
    public class ChangeEmailRequestEmail : Event
    {
        public ChangeEmailRequestEmail(Guid aggregateRootId, string newEmail) 
            : base(aggregateRootId)
        {
            NewEmail = newEmail;
        }

        public string NewEmail { get; private set; }
    }
}