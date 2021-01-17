using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace UserService.Domain.Common
{
    public class EncryptionService<TKey> : IEncryptionService<TKey>
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IEncryptionKeysProvider<TKey> _encryptionKeysProvider;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public EncryptionService(IDataProtectionProvider dataProtectionProvider, IEncryptionKeysProvider<TKey> encryptionKeysProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _encryptionKeysProvider = encryptionKeysProvider;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }
        public async Task<EncryptedEvent<TKey>> EncryptAsync(IEvent<TKey> eventToEncrypt, CancellationToken token)
        {
            var key = await _encryptionKeysProvider.GetEncryptionKeyAsync(eventToEncrypt.AggregatorId, token);
            var protector = _dataProtectionProvider.CreateProtector(key);

            var serializedEvent = JsonConvert.SerializeObject(eventToEncrypt, _jsonSerializerSettings);
            var encrypted = protector.Protect(serializedEvent);
            
            return new EncryptedEvent<TKey>(eventToEncrypt.AggregatorId, encrypted);
        }

        public async Task<IEvent<TKey>> DecryptAsync(EncryptedEvent<TKey> encryptedEvent, CancellationToken token)
        {
            var key = await _encryptionKeysProvider.GetEncryptionKeyAsync(encryptedEvent.AggregatorId, token);
            var protector = _dataProtectionProvider.CreateProtector(key);

            var decrypted = protector.Unprotect(encryptedEvent.EncryptedData);

            return JsonConvert.DeserializeObject<IEvent<TKey>>(decrypted, _jsonSerializerSettings);
        }
    }
}