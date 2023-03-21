using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Financio
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CollectionIds { get; set; }
        [BsonIgnore]
        public List<Collection> Collections { get; set; }
    }
}
