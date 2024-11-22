using Data.Models;
using Dto;

namespace Mapper.UserMapper
{
    public class UserMapper
    {
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
                
            }
        }
    }
}
