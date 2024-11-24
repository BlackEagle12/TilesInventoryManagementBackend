using Data.Models;

namespace Repo
{
    public interface IUserRepository : IBaseRepo<User>
    {
        Task<bool> IsUserExistAsync(User user);
        Task<User> GetUserAsync(int id);
        Task<List<User>> GetUsersPageAsync(int? pageNo, int? pageSize);
        Task<bool> IsEmailExistAsync(string email);
        Task<bool> IsUserNameExistAsync(string userName);
        Task<bool> IsPhoneExistAsync(string phoneNo);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(int id, User updatedUser);
        Task<User> DeleteUserAsync(int id);
    }
}
