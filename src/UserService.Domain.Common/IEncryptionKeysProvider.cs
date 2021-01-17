using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Domain.Common
{
    public interface IEncryptionKeysProvider<in TKey>
    {
        Task<string> GetEncryptionKeyAsync(TKey aggregateId, CancellationToken token);
    }
}