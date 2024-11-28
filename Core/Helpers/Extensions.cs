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

        public static string ToLikeFilterString(this string value, Operator compareOperator)
        {
            var retVal = value.Replace("[", "[[]")
                                     .Replace("_", "[_]")
                                     .Replace("%", "[%]");

            retVal = compareOperator switch
            {
                Operator.Contains => retVal = $"%{value}%",
                Operator.StartsWith => retVal = $"{value}%",
                Operator.EndsWith => retVal = $"%{value}",
                _ => retVal = retVal.Trim()
            };

            return retVal;
        }
    }
}
