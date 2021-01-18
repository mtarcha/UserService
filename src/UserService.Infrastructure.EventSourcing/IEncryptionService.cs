using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Infrastructure.EventSourcing
{
    public interface IEncryptionService<TKey>
    {
        Task<EncryptedEvent<TKey>> EncryptAsync(IEvent<TKey> eventToEncrypt, CancellationToken token);

        Task<IEvent<TKey>> DecryptAsync(EncryptedEvent<TKey> encryptedEvent, CancellationToken token);
    }
}