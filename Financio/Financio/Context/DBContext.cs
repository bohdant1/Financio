using MongoDB.Driver;

namespace Financio
{
    public class DBContext
    {
        public DBContext(IConfiguration configuration)
        {
            string connectionUri = configuration.GetValue<string>("MongoDb:ConnectionString");
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDb:DatabaseName"));

            Articles = database.GetCollection<Article>("Articles");
            Collections = database.GetCollection<Collection>("Collections");
        }

        public IMongoCollection<Article> Articles { get; }
        public IMongoCollection<Collection> Collections { get; }
    }
}
