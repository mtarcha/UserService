using System.Collections.Generic;

namespace UserService.Domain.Common
{
    public interface IAggregate<TKey>
    {
        TKey Id { get; }

        IReadOnlyCollection<IEvent<TKey>> ChangeSet { get; }
    }
}