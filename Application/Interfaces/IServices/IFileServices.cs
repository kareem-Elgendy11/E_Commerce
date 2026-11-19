using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices
{
    public interface IFileServices
    {
        public Task<string> SaveImage(IFormFile file, string FolderName);
        public Task<bool> DeleteImage(string ImageUrl);

    }
}
