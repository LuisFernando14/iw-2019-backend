using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TutorialMoya001.Repositories
{
    public class ImagenesRepository : IImagenesRepository
    {
        private readonly string ConnectionString;

        public ImagenesRepository(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            // https://www.ttmind.com/TechPost/Images-Upload-REST-API-using-ASP-NET-Core
        }

        public async Task<string> SaveImage(string name, string ContentType, Stream image)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.ConnectionString);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("images");
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(name);
            cloudBlockBlob.Properties.ContentType = ContentType;
            await cloudBlockBlob.UploadFromStreamAsync(image);
            return cloudBlockBlob.StorageUri.ToString();
        }
    }
}
