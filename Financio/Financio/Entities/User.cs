using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Financio.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
