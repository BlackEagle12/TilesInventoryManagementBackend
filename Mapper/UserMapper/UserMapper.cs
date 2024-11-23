using Core;
using Data.Models;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mapper.UserMapper
{
    public class UserMapper
    {
        private readonly AppSettings _appSettings;
        private readonly HttpContext? _httpContext;

        public UserMapper(AppSettings appSttings, IHttpContextAccessor contextAccessor)
        {
            _appSettings = appSttings;
            _httpContext = contextAccessor.HttpContext;
        }

        public UserDto GetUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNo = user.PhoneNo,
                Address1 = user.Address1,
                Address2 = user.Address2,

                //Country = user.CountryId
                //State = user.StateId

                City = user.City,
                Pincode = user.Pincode,
                Summary = user.Summary,
                BirthDate = user.BirthDate,
                AniversaryDate = user.AniversaryDate,

                //Role = user.RoleId,
                //CategoryId = user.CategoryId,

                AddedOn = user.AddedOn,

            };
        }


        public string GenerateAuthToken(User user, Role role, List<Permission> permissions)
        {
            var secretKey = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                new(ClaimTypes.Name, user.Username, ClaimValueTypes.String),
                new(ClaimTypes.Surname, user.LastName, ClaimValueTypes.String),
                new(ClaimTypes.GivenName, user.FirstName, ClaimValueTypes.String),
                new(ClaimTypes.Email, user.Email, ClaimValueTypes.String),
                new(ClaimTypes.Role, role.RoleName, ClaimValueTypes.String),
                new("RoleId", user.RoleId.ToString(), ClaimValueTypes.Integer),
                new(ClaimTypes.MobilePhone, user.PhoneNo.ToString(), ClaimValueTypes.Integer),
            };

            claims.AddRange(permissions.Select(x => new Claim(ClaimTypes.UserData, x.PermissionName.ToString(), ClaimValueTypes.String)));


            var tokenDescriptor = new JwtSecurityToken(
                issuer: _appSettings.APIUrl,
                audience: _httpContext?.Request.Host.Value, // TODO: need to check
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(_appSettings.TokenExpiryHours),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.Aes256CbcHmacSha512),
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
