using ErrorOr;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TunzWorkout.Application.Common.Services.Files;

namespace TunzWorkout.Infrastructure.Files
{
    public class VideoFileService : IVideoFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public VideoFileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<ErrorOr<string>> SaveVideoAsync(IFormFile videoFile, string[] allowedVideoExtensions, string type)
        {
            var path = Path.Combine(_configuration["UploadSettings:UploadVideoPath"], type);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(videoFile.FileName);
            if (!allowedVideoExtensions.Contains(ext))
            {
                var extAllowed = string.Join(",", allowedVideoExtensions);
                return Error.Conflict(description: $"Only {extAllowed} are allowed.");
            }
            var videoId = Guid.NewGuid();
            var fileName = $"{videoId.ToString()}{ext}";
            var relativePath = Path.Combine(type, fileName); //Exercise/GuidID.ext
            var fileNameWithPath = Path.Combine(path, fileName); //F:asdasd/...
            try
            {
                using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                await videoFile.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                return Error.Conflict(description: $"Error saving file: {ex}");
            }
            return relativePath;
        }

        public async Task<ErrorOr<Deleted>> DeleteVideoAsync(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                return Error.NotFound(description: "File name required");
            }
            var contentPath = Path.Combine(_configuration["UploadSettings:UploadVideoPath"]);
            var path = Path.Combine(contentPath, fileNameWithExtension);
            if (!File.Exists(path))
            {
                return Error.NotFound(description: "File not found");
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
