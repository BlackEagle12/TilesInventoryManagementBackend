
using Data.Models;
using Dto;

namespace Service
{
    public interface IUserService
    {
        public User AddUser(UserDto user);
    }
}
