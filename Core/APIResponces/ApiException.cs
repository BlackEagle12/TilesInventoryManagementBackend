using System.Diagnostics.CodeAnalysis;


namespace Core
{
    public class ApiException : Exception
    {
        [NotNull]
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public ApiException(int httpStatusCode, string errorMessage)
        {
            ErrorMessage = errorMessage;
            StatusCode = httpStatusCode;
        }
    }
}
