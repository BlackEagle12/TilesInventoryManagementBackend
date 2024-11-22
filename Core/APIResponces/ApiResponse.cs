using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Http;

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
