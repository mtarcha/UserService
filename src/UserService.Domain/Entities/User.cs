using System;
using System.Collections.Generic;
using UserService.Domain.Common;
using UserService.Domain.Events;

namespace UserService.Domain.Entities
{
    public class User : IAggregateRoot<Guid>
    {
        private readonly List<IEvent<Guid>> _changeSet;

        public User()
        {
            _changeSet = new List<IEvent<Guid>>();
        }

        public User(string email)
        {
            Id = Guid.NewGuid();
            Email = email;
            IsEmailVerified = false;

            _changeSet = new List<IEvent<Guid>>
            {
                new CreateUserRequestEvent(Id, email),
            };
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public bool IsEmailVerified { get; private set; }

        public bool IsDeleted { get; private set; }

        public IReadOnlyCollection<IEvent<Guid>> ChangeSet => _changeSet;

        public void ChangeEmail(string newEmail)
        {
            _changeSet.Add(new ChangeEmailRequestEmail(Id, newEmail));
            Email = newEmail;
            IsEmailVerified = false;
        }

        public void SetEmailVerified()
        {
            _changeSet.Add(new UserEmailVerifiedEvent(Id));
            IsEmailVerified = true;
        }

        public void Delete()
        {
            _changeSet.Add(new DeleteUserRequestEvent(Id));
            IsDeleted = true;
        }
    }
}