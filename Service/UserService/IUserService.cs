
using Data.Models;
using Dto;

namespace Service
{
    public interface IUserService
    {
        Task<UserDto> AddOrUpdateUser(UserDto user);
        Task<UserDto> GetUser(int id);
        Task<List<UserDto>> GetUsersPage(CommonDto commonDto);
    }
}
