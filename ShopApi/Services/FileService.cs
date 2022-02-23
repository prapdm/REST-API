using Microsoft.AspNetCore.Http;
using System.IO;

namespace ShopApi.Services
{
    public interface IFileService
    {
        bool Upload(IFormFile file);
    }
    public class FileService : IFileService
    {
        public FileService()
        {

        }


        public bool Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/wwwroot/images/{fileName}";
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return true;
            }

            return false;
        }
    }
}
