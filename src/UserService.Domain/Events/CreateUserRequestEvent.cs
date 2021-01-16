using System;
using UserService.Domain.Common;

namespace UserService.Domain.Events
{
    public class CreateUserRequestEvent : Event
    {
        public CreateUserRequestEvent(Guid userId, string email)
            : base(userId)
        {
            Email = email;
        }

        public string Email { get; private set; }
    }
}