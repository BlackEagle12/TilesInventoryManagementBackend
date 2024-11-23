using Data.Models;
using Dto;

namespace Repo
{
    public interface IUserRepository : IBaseRepo<User>
    {
        Task<bool> IsUserExist(UserDto userDto);
        Task<User> GetUser(int id);
        Task<List<User>> GetUsersPage(int pageNo, int pageSize);
    }
}
