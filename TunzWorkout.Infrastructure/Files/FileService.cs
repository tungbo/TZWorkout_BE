using ErrorOr;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace TunzWorkout.Application.Common.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public FileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<ErrorOr<string>> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions, string type)
        {
            if(imageFile is null)
            {
                return Error.Conflict(description: "Image file not found");
            }
            var path = Path.Combine(_configuration["UploadSettings:UploadPath"], type);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(imageFile.FileName);
            if(!allowedFileExtensions.Contains(ext))
            {
                var extAllowed = string.Join(",", allowedFileExtensions);
                return Error.Conflict(description: $"Only {extAllowed} are allowed.");
            }

            var fileId = Guid.NewGuid();
            var fileName = $"{fileId.ToString()}{ext}";
            var relativePath = Path.Combine(type, fileName); //Muscle/guidID.ext
            var fileNameWithPath = Path.Combine(path, fileName); //F:asdasd/...
            try
            {
                using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                await imageFile.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                return Error.Conflict(description: $"Error saving file: {ex}");
            }
            return relativePath;
        }

        public async Task<ErrorOr<Deleted>> DeleteFileAsync(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                return Error.Conflict(description: "Invalid file name");
            }

            var contentPath = Path.Combine(_configuration["UploadSettings:UploadPath"]);
            var path = Path.Combine(contentPath, fileNameWithExtension);
            if(!File.Exists(path))
            {
                return Error.Conflict(description: "File not found");
            }

            try
            {
                File.Delete(path);
                await Task.CompletedTask;
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Conflict(description: $"Error deleting file: {ex}");
            }
        }

    }
}
