using System.Threading;
using System.Threading.Tasks;

namespace UserService.Domain.Common
{
    public interface IEncryptionService<TKey>
    {
        Task<EncryptedEvent<TKey>> EncryptAsync(IEvent<TKey> eventToEncrypt, CancellationToken token);

        Task<IEvent<TKey>> DecryptAsync(EncryptedEvent<TKey> encryptedEvent, CancellationToken token);
    }
}