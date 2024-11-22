using System.Linq.Expressions;
using Core;
using Data.Contexts;
using Data.Models;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Repo.UserRepo
{
    public class UserRepository : BaseRepo<User>, IUserRepository
    {
        public UserRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }

        public async Task<bool> IsUserExist(UserDto userDto)
        {
            var getExistingUserFilter = GetExistingUserFilter(userDto);
            var userList = await Select(getExistingUserFilter).ToListAsync();
            return userList.Count == 0;
        }

        public async Task<User> GetUser(int id)
        {
            return await GetByIdAsync(id) ?? throw new ApiException(StatusCodes.Status404NotFound, "User Not Found");
        }

        public async Task<List<User>> GetUsersPage(int pageNo, int pageSize)
        {
            return await GetQueyable().Skip(pageNo * pageSize).Take(pageSize).ToListAsync();
        }

        private Expression<Func<User, bool>> GetExistingUserFilter(UserDto userDto)
        {
            var userPradicate = PradicateBuilder.True<User>();

            return userPradicate
                    .And(x => x.Email.Equals(userDto.Email) ||
                        x.Equals(userDto.Username) ||
                        x.Equals(userDto.PhoneNo));
        }
    }
}
