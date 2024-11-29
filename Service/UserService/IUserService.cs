using Core;
using Dto;

namespace Service
{
    public interface IUserService
    {
        Task AddUserAsync(UserDto user);
        Task<UserDto> GetUserAsync(int id);
        Task<KeyValuePair<int, List<UserDto>>> GetUsersPageAsync(CommonGridParams gridParams);
        Task<bool> IsUserExistAsync(UserDto userDto);
        Task<bool> IsEmailExistAsync(string email);
        Task<bool> IsUserNameExistAsync(string email);
        Task<bool> IsPhoneExistAsync(string email);
        Task UpdateUserAsync(int id, UserDto userDto);
        Task<UserDto> DeleteUserAsync(int id);
        Task<UserDto> LoginAsync(string userName, string password);
    }
}
