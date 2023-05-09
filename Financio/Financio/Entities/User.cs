using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Financio
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<ObjectId> LikedArticles { get; set; }
    }
}
