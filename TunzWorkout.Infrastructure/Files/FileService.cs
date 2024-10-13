using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TunzWorkout.Domain.Entities.Images;
using TunzWorkout.Infrastructure.Data;
namespace TunzWorkout.Application.Common.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        public FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Guid> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions, string type, Guid imageableId)
        {
            if(imageFile is null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }
            var path = Path.Combine(_configuration["UploadSettings:UploadPath"], type);

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
            var relativePath = Path.Combine(type, fileName);
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            Image image = new()
            {
                Id = filedId,
                ImagePath = relativePath,
                UploadDate = DateTime.Now,
                Type = type,
                ImageableId = imageableId,
            };
            await _dbContext.Images.AddAsync(image);
            return filedId;
        }

        public void DeleteFileAsync(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }

            var contentPath = Path.Combine(_configuration["UploadSettings:UploadPath"]);
            var path = Path.Combine(contentPath, fileNameWithExtension);
            if(!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }

            File.Delete(path);
        }

    }
}
