using Core;
using Data.Models;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mapper
{
    public class UserMapper
    {
        private readonly AppSettings _appSettings;
        private readonly HttpContext? _httpContext;

        public UserMapper(IOptions<AppSettings> appSttings, IHttpContextAccessor contextAccessor)
        {
            _appSettings = appSttings.Value;
            _httpContext = contextAccessor.HttpContext;
        }


        public User GetUser(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Username = userDto.Email,
                Password = userDto.Password,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PhoneNo = userDto.PhoneNo,
                Address1 = userDto.Address1,
                Address2 = userDto.Address2,
                CountryId = userDto.CountryId,
                StateId = userDto.StateId,
                City = userDto.City,
                Pincode = userDto.Pincode,
                Summary = userDto.Summary,
                BirthDate = userDto.BirthDate,
                AniversaryDate = userDto.AnniversaryDate,
                RoleId = userDto.RoleId,
                CategoryId = userDto.CategoryId,
                AddedOn = userDto.AddedOn,
                LastUpdatedOn = userDto.LastUpdatedOn
            };
        }

        public UserDto GetUserDto(
                User user, 
                Country? userCountry = null, 
                State? userState = null, 
                Role? userRole = null, 
                Category? userCategory = null,
                bool includeToken = false
            )
        {
            var dto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNo = user.PhoneNo,
                Address1 = user.Address1,
                Address2 = user.Address2,
                CountryId = user.CountryId,
                Country = userCountry?.CountryName,
                StateId = user.StateId,
                State = userState?.StateName,
                City = user.City,
                Pincode = user.Pincode,
                Summary = user.Summary,
                BirthDate = user.BirthDate,
                AnniversaryDate = user.AniversaryDate,
                RoleId = user.RoleId,
                Role = userRole?.RoleName,
                CategoryId = user.Id,
                Category = userCategory?.CategoryName,
                AddedOn = user.AddedOn

            };

            if (includeToken)
                dto.Token = GenerateAuthToken(dto);

            return dto;
        }


        public string GenerateAuthToken(UserDto user)
        {
            var secretKey = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                new(ClaimTypes.Name, user.Username, ClaimValueTypes.String),
                new(ClaimTypes.Surname, user.LastName, ClaimValueTypes.String),
                new(ClaimTypes.GivenName, user.FirstName, ClaimValueTypes.String),
                new(ClaimTypes.Email, user.Email, ClaimValueTypes.String),
                new(ClaimTypes.Role, user.Role!, ClaimValueTypes.String),
                new("RoleId", user.RoleId.ToString(), ClaimValueTypes.Integer),
                new(ClaimTypes.MobilePhone, user.PhoneNo.ToString(), ClaimValueTypes.Integer),

                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user), JsonClaimValueTypes.Json)
            };

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
