using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BeComfy.Common.Mongo
{
    public static class Extensions
    {
        private static readonly string mongoSection = "mongo";
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
        
            var mongoOptions = configuration.GetOptions<MongoOptions>(mongoSection);
            services.Configure<MongoOptions>(configuration.GetSection(mongoSection));
        
            var mongoSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(mongoOptions.ConnectionString),
                ConnectTimeout = TimeSpan.FromSeconds(mongoOptions.ConnectionTimeout)
            };

            services.AddSingleton<IMongoClient>(x => new MongoClient(mongoSettings));   
            RegisterConventions();

            return services; 
        }

        public static void AddMongoRepository<TEntity>(this IServiceCollection services, string collectionName) 
            where TEntity : IEntity
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var mongoClient = serviceProvider.GetService<IMongoClient>();
                var mongoOptions = serviceProvider.GetService<IOptions<MongoOptions>>().Value;
                var db = mongoClient.GetDatabase(mongoOptions.DatabaseName);
                services.AddTransient<IMongoRepository<TEntity>>(x => new MongoRepository<TEntity>(db, collectionName));
            }
        }

        private static void RegisterConventions()
        {
            BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            ConventionRegistry.Register("Conventions", new MongoDbConventions(), x => true);
        }

        private class MongoDbConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}