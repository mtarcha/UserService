using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Infrastructure.EventSourcing
{
    public interface IEventStore<TKey>
    {
        Task AddEventsAsync(IEnumerable<IEvent<TKey>> events, CancellationToken token);

        Task<IReadOnlyList<IEvent<TKey>>> GetEventsAsync(TKey aggregateId, CancellationToken token);
    }
}