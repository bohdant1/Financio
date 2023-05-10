using MongoDB.Bson.IO;
using MongoDB.Bson;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Financio
{
    public class MessageBrokerContext
    {
        private IConfiguration _configuration;
        private string connectionString;
        private string username;
        private string password;

        public MessageBrokerContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetValue<string>("MessageBroker:ConnectionString");
            username = _configuration.GetValue<string>("MessageBroker:Username");
            password = _configuration.GetValue<string>("MessageBroker:Password");
        }

        public void PublishEventArticleCreated(Article article)
        {
            var factory = new ConnectionFactory() { HostName = connectionString, UserName = username, Password = password };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "article_created", durable: false, exclusive: false, autoDelete: false, arguments: null);

            string message = JsonSerializer.Serialize(article);
            var body = Encoding.Unicode.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "article_created", basicProperties: null, body: body);
            Console.WriteLine("Article created message sent: {0}", message);
        }
    }
}
