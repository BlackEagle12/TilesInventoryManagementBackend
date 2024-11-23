using Core;
using Dto;
using Repo;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(
                IUserRepository userRepository
            )
        {
            _userRepo = userRepository;
        }

        public async Task<UserDto> AddOrUpdateUser(UserDto userDto)
        {
            var IsUserExist = await _userRepo.IsUserExist(userDto);

            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _userRepo.GetUser(id);

            return user ?? throw new ApiException(400, $"No User Found with Id: {id}");
        }

        public async Task<List<UserDto>> GetUsersPage(CommonDto commonDto)
        {
            return await _userRepo.GetUsersPage(commonDto.PageNo, commonDto.PageSize);
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
