using Core;
using Data.Models;
using Dto;
using Repo;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User AddUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> Login(string userName, string password) 
        { 
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new ApiException(401, $"Invalid {nameof(userName)} or {nameof(password)}");

            return await Task.FromResult(new UserDto());

            //var user = _userRepo.
        }
    }
}
