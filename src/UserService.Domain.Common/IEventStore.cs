using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Domain.Common
{
    public interface IEventStore<TKey>
    {
        Task AddEventsAsync(IEnumerable<IEvent<TKey>> events, CancellationToken token);

        Task<IReadOnlyCollection<IEvent<TKey>>> GetEventsAsync(TKey aggregateId, CancellationToken token);
    }
}