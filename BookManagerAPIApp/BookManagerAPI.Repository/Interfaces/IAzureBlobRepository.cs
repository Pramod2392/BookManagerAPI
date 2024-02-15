using Microsoft.AspNetCore.Http;

namespace BookManagerAPI.Repository.Interfaces
{
    public interface IAzureBlobRepository
    {
        Task<string> UploadImageToBlobAsync(IFormFile formFile);
    }
}