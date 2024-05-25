using Azure.Storage.Blobs;
using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Impl
{
    public class AzureBlobRepository : IAzureBlobRepository
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<AzureBlobRepository> _logger;
        private readonly IConfiguration _configuration;

        public AzureBlobRepository(BlobServiceClient blobServiceClient, ILogger<AzureBlobRepository> logger, IConfiguration configuration )
        {
            this._blobServiceClient = blobServiceClient;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<UploadImageToBlobAsyncResponseModel> UploadImageToBlobAsync(IFormFile formFile, string userId)
        {
            string blobName;
            try
            {
                using var fileStream = formFile.OpenReadStream();
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_configuration["BlobConfiguration:ContainerName"]);
                blobName = FrameBlobName(userId, formFile.FileName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                var response = await blobClient.UploadAsync(fileStream, true);
                if (response.GetRawResponse().Status == 201)
                {
                    return new UploadImageToBlobAsyncResponseModel(true, response.GetRawResponse().ReasonPhrase,blobName);
                }
                else
                {
                    _logger.LogInformation(response.GetRawResponse().ReasonPhrase);
                    return new UploadImageToBlobAsyncResponseModel(false, response.GetRawResponse().ReasonPhrase, string.Empty);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while uploading the image to the blob container");
                throw;
            }
        }


        public async Task<byte[]> GetImageFromBlob(string blobURL)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_configuration["BlobConfiguration:ContainerName"]);
                var splitArray = blobURL.Split('/');
                var blobName = splitArray[splitArray.Length - 2] + '/' + splitArray[splitArray.Length - 1];
                var blobClient = blobContainerClient.GetBlobClient(blobName);
                var downloadResult = await blobClient.DownloadContentAsync();
                return downloadResult.Value.Content.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while fetching image from blob. {ex.Message}");
                throw;
            }            
        }

        
        private string FrameBlobName(string folderName, string fileName)
        {            
            var blobName = $"{folderName}/{fileName}";
            return blobName;
        }
    }
}
