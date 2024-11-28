using Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _settings;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _settings = new JsonSerializerSettings()
            {
                Culture = CultureInfo.InvariantCulture,
                DateFormatString = "dddd, dd, MMMM, yyyy hh:mm:ss tt K",
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,

            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {

                var text = JsonConvert.SerializeObject(new ApiResponse(
                            ex.StatusCode,
                            null,
                            ex.ErrorMessage
                        ), _settings);

                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsync(text);
            }
            catch (Exception ex)
            {
                var text = JsonConvert.SerializeObject(new ApiResponse(
                        StatusCodes.Status500InternalServerError,
                        null,
                        ex.Message
                    ), _settings);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(text);
            }
        }
    }
}
