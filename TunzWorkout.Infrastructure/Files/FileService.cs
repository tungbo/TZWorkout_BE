using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace TunzWorkout.Application.Common.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Guid> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions)
        {
            if(imageFile is null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            var contentPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(imageFile.FileName);
            if(!allowedFileExtensions.Contains(ext))
            {
                throw new AggregateException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
            }
            var filedId = Guid.NewGuid();
            var fileName = $"{filedId.ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            return filedId;
        }

        public void DeleteFileAsync(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }

            var contentPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, $"Uploads", fileNameWithExtension);
            if(!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }

            File.Delete(path);
        }

    }
}
