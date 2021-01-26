using System.Threading;
using System.Threading.Tasks;

namespace UserService.Domain.Common
{
    public interface IRepository<TAggregateRoot, TId>
        where TAggregateRoot : IAggregateRoot<TId>
    {
        Task<TAggregateRoot> GetByIdAsync(TId id, CancellationToken token);

        Task SaveChangesAsync(TAggregateRoot aggregate, CancellationToken token);
    }
}