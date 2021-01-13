using System;
using System.Threading.Tasks;

namespace UserService.Domain.Common
{
    public interface IEvent<out TKey>
    {
        TKey AggregatorId { get; }

        DateTime Timestamp { get; }
    }
}