using System.Diagnostics.CodeAnalysis;

namespace Core
{
    public class ApiResponse
    {
        [NotNull]
        public int StatusCode { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int httpStatusCode, object? data = null, string? message = null)
        {
            Data = data;
            StatusCode = httpStatusCode;
            Message = message;
        }
    }
}
