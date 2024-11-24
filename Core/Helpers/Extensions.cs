using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Core
{
    public static class Extensions
    {
        public static Task<TokenValidationResult> ValidateToken(this string token, IEnumerable<string> validAudiences, string validIssuer)
        {
            if (string.IsNullOrEmpty(token))
                throw new ApiException(StatusCodes.Status401Unauthorized, "Token can not be Empty");

            TokenValidationParameters tokenParams = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                //ValidAudience = validAudience,
                ValidAudiences = validAudiences,
                ValidIssuer = validIssuer,
            };

            var result = new JwtSecurityTokenHandler().ValidateTokenAsync(token, tokenParams);

            return result;
        }
    }
}
