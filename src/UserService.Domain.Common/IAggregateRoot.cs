using System.Collections.Generic;

namespace UserService.Domain.Common
{
    public interface IAggregateRoot<TId>
    {
        TId Id { get; }

        IReadOnlyCollection<IEvent<TId>> UncommittedChanges { get; }
    }
}