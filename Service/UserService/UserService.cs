using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Dto;
using Repo.UserRepo;
;

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

        }

        public async Task<List<UserDto>> GetUsersPage(CommonDto commonDto)
        {
            return await _userRepo.GetUsersPage(commonDto.PageNo, commonDto.PageSize);
        }
    }
}
