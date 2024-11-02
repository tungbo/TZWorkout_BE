using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace TunzWorkout.Application.Common.Services.Files
{
    public interface IFileService
    {
        Task<ErrorOr<string>> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions, string type);
        Task<ErrorOr<Deleted>> DeleteFileAsync(string fileNameWithExtension);
    }
}
