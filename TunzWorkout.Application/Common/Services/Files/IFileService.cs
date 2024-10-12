using Microsoft.AspNetCore.Http;

namespace TunzWorkout.Application.Common.Services.Files
{
    public interface IFileService
    {
        Task<Guid> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions, string type, Guid imageableId);
        void DeleteFileAsync(string fileNameWithExtension);
    }
}
