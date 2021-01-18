using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using UserService.Infrastructure.EventSourcing;

namespace UserService.Infrastructure.Mongo
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoDbEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDatabaseSettings>(
                configuration.GetSection(nameof(MongoDatabaseSettings)));

            services.AddScoped<IEventStore<Guid>, EventStore>();

            BsonClassMap.RegisterClassMap<EncryptedEvent<Guid>>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}