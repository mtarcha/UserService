using System;
using System.Threading.Tasks;

namespace UserService.Domain.Common
{
    public interface IEvent<TKey>
    {
        TKey AggregatorId { get; set; }

        DateTime Timestamp { get; set; }
    }
}