using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserService.Domain.Common;
using UserService.Domain.Events;
using UserService.Infrastructure.EventSourcing;

namespace UserService.Infrastructure.Mongo
{
    public class EventStore : IEventStore<Guid>
    {
        private readonly IEncryptionService<Guid> _encryptionService;
        private readonly IMongoCollection<EncryptedEvent<Guid>> _mongoCollection;

        public EventStore(IOptions<MongoDatabaseSettings> options, IEncryptionService<Guid> encryptionService)
        {
            var settings = options.Value;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mongoCollection = database.GetCollection<EncryptedEvent<Guid>>(settings.CollectionName);

            _encryptionService = encryptionService;
        }

        public async Task AddEventsAsync(IEnumerable<IEvent<Guid>> events, CancellationToken token)
        {
            var encryptedEvents = new List<EncryptedEvent<Guid>>();
            foreach (var @event in events)
            {
                var encrypted = await _encryptionService.EncryptAsync(@event, token);
                encryptedEvents.Add(encrypted);
            }

            await _mongoCollection.InsertManyAsync(encryptedEvents, cancellationToken: token);
        }

        public async Task<IReadOnlyCollection<IEvent<Guid>>> GetEventsAsync(Guid aggregateId, CancellationToken token)
        {
            var filter = new FilterDefinitionBuilder<EncryptedEvent<Guid>>().Eq(nameof(Event.AggregateRootId), aggregateId);
            var encryptedEvents = await _mongoCollection.Find(filter).ToListAsync(token);

            var decryptedEvents = new List<IEvent<Guid>>();
            foreach (var encryptedEvent in encryptedEvents)
            {
                var decrypted = await _encryptionService.DecryptAsync(encryptedEvent, token);
                decryptedEvents.Add(decrypted);
            }

            return decryptedEvents;
        }
    }
}