﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CampAzureApp.Options;
using CampAzureApp.Services.Abstract;
using Microsoft.Extensions.Options;

namespace CampAzureApp.Services.Concrete
{
    public class DocumentService : IDocumentService
    {
        private readonly AzureOptions _azureOptions;
        public DocumentService(IOptions<AzureOptions> azureOptions)
        {
            _azureOptions = azureOptions.Value;
        }

        public void UploadDocumentToAzure(IFormFile file, string userEmail)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            using MemoryStream fileUploadStream = new MemoryStream();
            file.CopyTo(fileUploadStream);
            fileUploadStream.Position = 0;
            BlobContainerClient blobContainerClient = new BlobContainerClient(
                _azureOptions.ConnectionString,
                _azureOptions.Container
                );

            Dictionary<string, string> metadata = new Dictionary<string, string>();
            metadata.Add("email", userEmail);

            var uniqueName = Guid.NewGuid().ToString() + fileExtension;
            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);

            blobClient.Upload(fileUploadStream, new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                },
                Metadata = metadata
            }, cancellationToken: default);
        }
    }
}
