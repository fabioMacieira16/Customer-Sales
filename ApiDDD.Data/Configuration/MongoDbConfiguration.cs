using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace ApiDDD.Data.Configuration
{
    public static class MongoDbConfiguration
    {
        public static void Configure()
        {
            // Configurar o MongoDB para usar Guid como string
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            // Configurar convenções
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register("CustomConventions", pack, t => true);
        }
    }
} 