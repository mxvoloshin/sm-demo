using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlazorApp.Api.Interfaces;
using BlazorApp.Shared;

namespace BlazorApp.Api.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobService(ICloudStorageSettings cloudStorageSettings)
        {
            var service = new BlobServiceClient(cloudStorageSettings.ConnectionString);
            _blobContainerClient = service.GetBlobContainerClient("images");
        }

        public async Task<Uri> UploadImageAsync(ImageDto imageDto)
        {
            var imagesBlob = _blobContainerClient.GetBlobClient(imageDto.Name);
            
            using (var stream = new MemoryStream(imageDto.Content))
            {
                var blobHttpHeader = new BlobHttpHeaders {ContentType = imageDto.ContentType};
                var result = await imagesBlob.UploadAsync(stream, blobHttpHeader);
            }

            return imagesBlob.Uri;
        }
    }
}
