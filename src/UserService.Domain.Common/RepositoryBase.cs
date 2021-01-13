using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Domain.Common
{
    public abstract class RepositoryBase<TAggregate, TKey> where TAggregate : IAggregate<TKey>
    {
        private readonly IEventStore<TKey> _eventStore;
        private readonly IEventDispatcher _eventDispatcher;

        protected RepositoryBase(IEventStore<TKey> eventStore, IEventDispatcher eventDispatcher)
        {
            _eventStore = eventStore;
            _eventDispatcher = eventDispatcher;
        }

        protected async Task SaveChanges(TAggregate aggregate)
        {
            var events = aggregate.ChangeSet;

            await _eventStore.AddEventsAsync(events, CancellationToken.None);

            foreach (var @event in events.OrderByDescending(x => x.Timestamp))
            {
                _eventDispatcher.PublishEvent(@event);
            }
        }
    }
}