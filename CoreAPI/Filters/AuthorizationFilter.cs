using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace API
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        private readonly AppSettings _appSettings;

        public AuthorizationFilter(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            AuthorizeTokenAsync(context).Wait();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await AuthorizeTokenAsync(context);
        }

        private async Task AuthorizeTokenAsync(AuthorizationFilterContext context)
        {
            try
            {
                var unauthorizedResult = new ObjectResult(
                                                                    new ObjectResult(new ApiResponse(
                                                                                401,
                                                                                null,
                                                                                "Invalid Token"
                                                                            ))
                                                                    {
                                                                        StatusCode = 401,
                                                                    });

                var authHeader = context.HttpContext?.Request?.Headers.Authorization.ToString();

                if (string.IsNullOrEmpty(authHeader))
                {
                    context.Result = unauthorizedResult;
                }

                var token = await authHeader?.Replace("Bearer ", string.Empty, StringComparison.OrdinalIgnoreCase)
                                                              .ValidateToken(_appSettings.ClientList, _appSettings.APIUrl)!;

                if (token == null || !token.IsValid)
                {
                    context.Result = unauthorizedResult;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Authorization failed", ex);
            }
        }

    }
}

