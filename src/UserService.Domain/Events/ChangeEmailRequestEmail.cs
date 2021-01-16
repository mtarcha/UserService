using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class ChangeEmailRequestEmail : Event
    {
        public ChangeEmailRequestEmail(Guid aggregatorId, string newEmail) 
            : base(aggregatorId)
        {
            NewEmail = newEmail;
        }

        public string NewEmail { get; private set; }
    }
}