using Microsoft.AspNetCore.Http;

namespace TunzWorkout.Application.Common.Services.Files
{
    public interface IFileService
    {
        Task<Guid> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions);
        void DeleteFileAsync(string fileNameWithExtension);
    }
}
