using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Financio
{
    public class BlobStorageContext
    {
        private IConfiguration _configuration;
        private string connectionString;
        private string? containerName;

        public BlobStorageContext(IConfiguration configuration)
        {
            _configuration= configuration;
            connectionString = _configuration.GetValue<string>("BlobStorage:ConnectionString");
        }

        public void SetContainer(string entityName)
        {
            containerName = _configuration.GetValue<string>($"BlobStorage:{entityName}Container");
        }


        public void Upload(string content, string id) 
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(id);
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                blobClient.Upload(stream);
            }
        }
    }
}
