using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Domain.Common;
using UserService.Domain.Events;

namespace UserService.Domain.Entities
{
    public class User : IAggregateRoot<Guid>
    {
        private readonly List<IEvent<Guid>> _uncommittedChanges;

        public User(IEnumerable<IEvent<Guid>> changeSet)
        {
            _uncommittedChanges = new List<IEvent<Guid>>();
            foreach (var @event in changeSet.OrderByDescending(x => x.Timestamp))
            {
                switch (@event)
                {
                    case CreateUserRequestEvent createUserRequestEvent:
                        Id = @event.AggregateRootId;
                        Email = createUserRequestEvent.Email; ;
                        break;
                    case ChangeEmailRequestEmail changeEmailRequestEmail:
                        Email = changeEmailRequestEmail.NewEmail;
                        break;
                    case UserEmailVerifiedEvent userEmailVerifiedEvent:
                        IsEmailVerified = true;
                        break;
                    case DeleteUserRequestEvent deleteUserRequestEvent:
                        throw new Exception($"User with Id {deleteUserRequestEvent.AggregateRootId} was deleted. Cannot return any data.");
                    default:
                        throw new NotSupportedException($"Event type {@event.GetType()} is not supported for user.");
                }
            }
        }

        public User(string email)
        {
            Id = Guid.NewGuid();
            Email = email;
            IsEmailVerified = false;

            _uncommittedChanges = new List<IEvent<Guid>>
            {
                new CreateUserRequestEvent(Id, email),
            };
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public bool IsEmailVerified { get; private set; }

        public IReadOnlyCollection<IEvent<Guid>> UncommittedChanges => _uncommittedChanges;

        public void ChangeEmail(string newEmail)
        {
            _uncommittedChanges.Add(new ChangeEmailRequestEmail(Id, newEmail));
            Email = newEmail;
            IsEmailVerified = false;
        }

        public void SetEmailVerified()
        {
            _uncommittedChanges.Add(new UserEmailVerifiedEvent(Id));
            IsEmailVerified = true;
        }

        public void Delete()
        {
            _uncommittedChanges.Add(new DeleteUserRequestEvent(Id));
        }
    }
}