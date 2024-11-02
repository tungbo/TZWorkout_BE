using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace TunzWorkout.Application.Common.Services.Files
{
    public interface IVideoFileService
    {
        Task<ErrorOr<string>> SaveVideoAsync(IFormFile videoFile, string[] allowedVideoExtensions, string type);
        Task<ErrorOr<Deleted>> DeleteVideoAsync(string fileNameWithExtension);
    }
}
