using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Financio
{
    public class Collection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
