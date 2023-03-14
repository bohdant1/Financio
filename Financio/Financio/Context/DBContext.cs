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

        }

        public IMongoCollection<Article> Articles { get; }
    }
}
