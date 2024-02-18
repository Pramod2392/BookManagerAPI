using BookManagerAPI.Repository.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace BookManagerAPI.Repository.Interfaces
{
    public interface IAzureBlobRepository
    {
        Task<UploadImageToBlobAsyncResponseModel> UploadImageToBlobAsync(IFormFile formFile);
    }
}