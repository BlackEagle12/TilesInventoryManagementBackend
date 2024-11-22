using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Dto;

namespace Repo.UserRepo
{
    public interface IUserRepository : IBaseRepo<User>
    {
        Task<bool> IsUserExist(UserDto userDto);
        Task<User> GetUser(int id);
        Task<List<User>> GetUsersPage(int pageNo, int pageSize);
    }
}
