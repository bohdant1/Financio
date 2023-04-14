using MongoDB.Driver;

namespace Financio
{
    public class DBContext
    {
        public DBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDb:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDb:DatabaseName"));

            Articles = database.GetCollection<Article>("Articles");
            Collections = database.GetCollection<Collection>("Collections");
        }

        public IMongoCollection<Article> Articles { get; }
        public IMongoCollection<Collection> Collections { get; }
    }
}
