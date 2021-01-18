using System.Threading;
using System.Threading.Tasks;

namespace UserService.Infrastructure.EventSourcing
{
    public interface IEncryptionKeysProvider<in TKey>
    {
        Task<string> GetOrCreateEncryptionKeyAsync(TKey aggregateId, CancellationToken token);
    }
}