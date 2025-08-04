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
                // Usar o nome da classe como prefixo para os campos
                new CamelCaseElementNameConvention(),
                //evita erro de campos que não existem no banco de dados
                new IgnoreExtraElementsConvention(true)
            };
            // Registrar as convenções
            ConventionRegistry.Register("CustomConventions", pack, t => true);
        }
    }
}