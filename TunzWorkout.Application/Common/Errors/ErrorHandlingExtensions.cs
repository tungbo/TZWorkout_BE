using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TunzWorkout.Application.Common.Errors
{
    public static class ErrorHandlingExtensions
    {
        public static IActionResult HandleError(List<Error> errors)
        {
            var firstError = errors.First();
            return firstError.Type switch
            {
                ErrorType.Conflict => new ObjectResult(new { detail = firstError.Description})
                {
                    StatusCode = StatusCodes.Status409Conflict
                },
                ErrorType.NotFound => new ObjectResult(new { detail = firstError.Description })
                {
                    StatusCode = StatusCodes.Status404NotFound
                },
                ErrorType.Validation => new BadRequestObjectResult(new
                {
                    Message = "Validation errors occurred.",
                    Errors = errors.Select(e => e.Description) // Liệt kê chi tiết các lỗi
                }),
                _ => new ObjectResult(new { detail = firstError.Description })
                {
                    StatusCode = StatusCodes.Status500InternalServerError // Mặc định Internal Server Error
                }
            };
        }
    }
}
