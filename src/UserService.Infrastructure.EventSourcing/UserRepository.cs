using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.EventSourcing
{
    public class UserRepository : IUserRepository
    {
        private readonly IEventStore<Guid> _eventStore;

        public UserRepository(IEventStore<Guid> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken token)
        {
            var userEvents = await _eventStore.GetEventsAsync(id, token).ConfigureAwait(false);
            return new User(userEvents);
        }

        public async Task SaveChangesAsync(User aggregate, CancellationToken token)
        {
            await _eventStore.AddEventsAsync(aggregate.UncommittedChanges, token).ConfigureAwait(false);
        }
    }
}