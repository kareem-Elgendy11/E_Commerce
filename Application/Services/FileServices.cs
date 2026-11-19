using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment env;

        public FileServices(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public async Task<bool> DeleteImage(string ImageUrl)
        {
            //throw new NotImplementedException();
            if (File.Exists(ImageUrl) == true)
            {
                File.Delete(ImageUrl);
                return true;
            }
            return false;
        }

        public async  Task<string> SaveImage(IFormFile file, string FolderName)
        {
            //throw new NotImplementedException();
            if(file.Length==0&&file==null)
            {
                return null!;
            }
            if(file.Length>2*1024*1024)
            {
                return  null!;
            }
            var pathnow = env.WebRootPath;
            var newfolder = Path.Combine(pathnow,FolderName);
            var extension = Path.GetExtension(file.FileName);
            var newfilename = $"{Guid.NewGuid().ToString()}{extension}";
            var NewfilePath = Path.Combine(newfolder, newfilename);

            var stream = new FileStream(NewfilePath, FileMode.Create);

             await file.CopyToAsync(stream);

            return  $"{NewfilePath}";

        }

    }
}
