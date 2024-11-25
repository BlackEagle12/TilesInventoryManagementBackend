using Data.Models;
using Dto;

namespace Mapper
{
    public class UserMapper
    {
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
                Category? userCategory = null
            )
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
        }
    }
}
