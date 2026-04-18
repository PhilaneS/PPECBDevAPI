using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IImageService
    {
        Task<(string url,string publicId)> UploadImageAsync(IFormFile file,int userId);
        Task DeleteAsync(string publicId);
    }
}
