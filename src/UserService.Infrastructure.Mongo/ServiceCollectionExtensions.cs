using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using UserService.Domain.Common;
using UserService.Domain.Events;

namespace UserService.Infrastructure.Mongo
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoDbEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDatabaseSettings>(
                configuration.GetSection(nameof(MongoDatabaseSettings)));

            services.AddScoped<IEventStore<Guid>, EventStore>();

            BsonClassMap.RegisterClassMap<Event>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.AddKnownType(typeof(ChangeEmailRequestEmail));
                cm.AddKnownType(typeof(CreateUserRequestEvent));
                cm.AddKnownType(typeof(DeleteUserRequestEvent));
                cm.AddKnownType(typeof(UserEmailVerifiedEvent));
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ChangeEmailRequestEmail>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<CreateUserRequestEvent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<DeleteUserRequestEvent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<UserEmailVerifiedEvent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}