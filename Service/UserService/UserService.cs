using Core;
using Data.Models;
using Dto;
using Mapper;
using Microsoft.EntityFrameworkCore;
using Repo;
using System.Linq.Expressions;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly UserDto _loggedInUser;

        private readonly UserMapper _userMapper;
        public UserService(
                IUserRepository userRepository,
                IRoleRepository roleRepository,
                ICategoryRepository categoryRepository,
                IStateRepository stateRepository,
                ICountryRepository countryRepository,
                IPermissionRepository permissionRepository,
                IRolePermissionRepository rolePermissionRepository,
                UserMapper userMapper,
                UserDto loggedInUser)
        {
            _userRepo = userRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
            _stateRepository = stateRepository;
            _countryRepository = countryRepository;
            _userMapper = userMapper;
            _loggedInUser = loggedInUser;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = _userMapper.GetUser(userDto);
            await _userRepo.AddUserAsync(user);
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await _userRepo.GetUserAsync(id);
            var userCountry = await _countryRepository.GetCountryAsync(user.CountryId);
            var userState = await _stateRepository.GetStateAsync(user.StateId);
            var userRole = await _roleRepository.GetRoleAsync(user.RoleId);
            var userCategory = await _categoryRepository.GetCategoryAsync(user.CategoryId);

            return _userMapper.GetUserDto(user, userCountry, userState, userRole, userCategory);
        }

        public async Task<List<UserDto>> GetUsersPageAsync(int? pageNo, int? pageSize)
        {
            var userList = await _userRepo.GetUsersPageAsync(pageNo, pageSize);

            var countryIdList = userList.Select(x => x.CountryId).Distinct().ToList();
            var countryDict = await _countryRepository.GetCountryDictAsync(countryIdList);

            var stateIdList = userList.Select(x => x.StateId).Distinct().ToList();
            var stateDict = await _stateRepository.GetStateDictAsync(stateIdList);

            var roleIdList = userList.Select(x => x.RoleId).Distinct().ToList();
            var roleDict = await _roleRepository.GetRoleDictAsync(roleIdList);

            var categoryIdList = userList.Select(x => x.CountryId).Distinct().ToList();
            var categoryDict = await _categoryRepository.GetCategoryDictAsync(categoryIdList);

            return
                userList
                    .Select(user =>
                        _userMapper.GetUserDto(
                            user, countryDict[user.CountryId],
                            stateDict[user.StateId],
                            roleDict[user.RoleId],
                            categoryDict[user.CategoryId])
                    )
                    .ToList();

        }

        public async Task<bool> IsUserExistAsync(UserDto userDto)
        {
            var user = _userMapper.GetUser(userDto);
            return await _userRepo.IsUserExistAsync(user);
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _userRepo.IsEmailExistAsync(email);
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            return await _userRepo.IsUserNameExistAsync(userName);
        }

        public async Task<bool> IsPhoneExistAsync(string phoneNo)
        {
            return await _userRepo.IsPhoneExistAsync(phoneNo);
        }

        public async Task UpdateUserAsync(int id, UserDto userDto)
        {
            var updatedUser = _userMapper.GetUser(userDto);
            await _userRepo.UpdateUserAsync(id, updatedUser);
        }

        public async Task<UserDto> DeleteUserAsync(int id)
        {
            var user = await _userRepo.DeleteUserAsync(id);
            return _userMapper.GetUserDto(user);
        }

        public async Task<UserDto> LoginAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new ApiException(401, $"Invalid {nameof(userName)} or {nameof(password)}");

            var userData = await GetUsersQuery(x => EF.Functions.Like(x.Username, userName)
                                                    || EF.Functions.Like(x.Email, userName)
                                                    || EF.Functions.Like(x.PhoneNo, userName)).FirstOrDefaultAsync();

            if (userData == null
               || userData.Item1 == null
               || string.IsNullOrEmpty(userData.Item1.Password)
               || !userData.Item1.Password.Equals(password))
            {
                throw new ApiException(401, "Invalid Username or Password");
            }

            return _userMapper.GetUserDto(userData.Item1, userData.Item2, userData.Item3, userData.Item4, userData.Item5, true);
        }

        private IQueryable<Tuple<User, Country?, State?, Role?, Category?>> GetUsersQuery(Expression<Func<User, bool>> filter)
        {
            var usersQuery = _userRepo.Get(filter, false);
            var countryQuery = _countryRepository.GetQueyable(false);
            var stateQuery = _stateRepository.GetQueyable(false);
            var roleQuery = _roleRepository.GetQueyable(false);
            var categoryQuery = _categoryRepository.GetQueyable(false);


            return usersQuery.GroupJoin(
                                            countryQuery,
                                            x => x.CountryId,
                                            y => y.Id,
                                            (User, countries) => new { User, countries }
                                        )
                             .SelectMany(
                                 x => x.countries.DefaultIfEmpty(),
                                 (result, Country) => new { result.User, Country }
                             )
                             .GroupJoin(
                                 stateQuery,
                                 x => x.User.StateId,
                                 y => y.Id,
                                 (result, states) => new { result.User, result.Country, states }
                             )
                             .SelectMany(
                                 x => x.states.DefaultIfEmpty(),
                                 (result, State) => new { result.User, result.Country, State }
                             )
                             .GroupJoin(
                                 roleQuery,
                                 x => x.User.RoleId,
                                 y => y.Id,
                                 (result, roles) => new { result.User, result.Country, result.State, roles }
                             )
                             .SelectMany(
                                 x => x.roles.DefaultIfEmpty(),
                                 (result, Role) => new { result.User, result.Country, result.State, Role }
                             )
                             .GroupJoin(
                                 categoryQuery,
                                 x => x.User.CategoryId,
                                 y => y.Id,
                                 (result, categories) => new { result.User, result.Country, result.State, result.Role, categories }
                             )
                             .SelectMany(
                                 x => x.categories.DefaultIfEmpty(),
                                 (result, Category) => new Tuple<User, Country?, State?, Role?, Category?>(
                                     result.User,
                                     result.Country,
                                     result.State,
                                     result.Role,
                                     Category
                                 )
                             );

        }

        public async Task<List<string>> GetUserPermissionAsync()
        {
            var permissionQuery = _permissionRepository.GetQueyable();
            var rolePermissionQuery = _rolePermissionRepository.GetQueyable();

            return
                await
                    permissionQuery
                        .Join(
                            rolePermissionQuery,
                            permission => permission.Id,
                            rolePermission => rolePermission.PermissionId,
                            (per, rolePer) => new { permission = per, roleId = rolePer.RoleId }
                        )
                        .Where(x => x.roleId.Equals(_loggedInUser.RoleId))
                        .Select(x => x.permission.PermissionName)
                        .ToListAsync();
        }
    }

}
