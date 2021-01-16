using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserService.Domain.Common;
using UserService.Domain.Events;

namespace UserService.Infrastructure.Mongo
{
    public class EventStore : IEventStore<Guid>
    {
        private readonly IMongoCollection<IEvent<Guid>> _mongoCollection;

        public EventStore(IOptions<MongoDatabaseSettings> options)
        {
            var settings = options.Value;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mongoCollection = database.GetCollection<IEvent<Guid>> (settings.CollectionName);
        }

        public async Task AddEventsAsync(IEnumerable<IEvent<Guid>> events, CancellationToken token)
        {
            await _mongoCollection.InsertManyAsync(events, cancellationToken: token);
        }

        public async Task<IReadOnlyCollection<IEvent<Guid>>> GetEventsAsync(Guid aggregateId, CancellationToken token)
        {
            var filter = new FilterDefinitionBuilder<IEvent<Guid>>().Eq(nameof(Event.AggregatorId), aggregateId);
            return await _mongoCollection.Find(filter).ToListAsync(token);
        }
    }
}