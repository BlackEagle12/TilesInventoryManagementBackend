using Core;
using Data.Models;
using Dto;
using Mapper;
using Microsoft.AspNetCore.Http;
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
        private readonly UserDto _loggedInUser;

        private readonly UserMapper _userMapper;
        public UserService(
                IUserRepository userRepository,
                IRoleRepository roleRepository,
                ICategoryRepository categoryRepository,
                IStateRepository stateRepository,
                ICountryRepository countryRepository,
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
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = _userMapper.GetUser(userDto);
            await _userRepo.AddUserAsync(user);
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            var userDto = await GetUserDtoQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (userDto == null)
                throw new ApiException(StatusCodes.Status400BadRequest, $"No User Found at Id: {id}");

            return await Task.FromResult(userDto);
        }

        public async Task<KeyValuePair<int, List<UserDto>>> GetUsersPageAsync(CommonGridParams gridParams)
        {
            //var userList = await _userRepo.GetUsersPageAsync(pageNo, pageSize);
            var userDtosQuery = GetUserDtoQuery();

            userDtosQuery = userDtosQuery
                .ApplyFilters(gridParams.Filters)
                .ApplySorting(gridParams.SortBy, gridParams.IsDescending);

            var result = await userDtosQuery.GetPaginatedAsync(gridParams.Page, gridParams.PageSize);

            return await Task.FromResult(new KeyValuePair<int,List<UserDto>>(result.Key, result.Value));

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

            var countryQuery = _countryRepository.GetQueyable(false);
            var stateQuery = _stateRepository.GetQueyable(false);
            var roleQuery = _roleRepository.GetQueyable(false);
            var categoryQuery = _categoryRepository.GetQueyable(false);


            var user = await GetUserDtoQuery().FirstOrDefaultAsync(x => EF.Functions.Like(x.Username, userName) 
                                                                || EF.Functions.Like(x.PhoneNo, userName) 
                                                                || EF.Functions.Like(x.Email, userName));

            if (user == null
               || !user.Password.Equals(password))
            {
                throw new ApiException(401, "Invalid Username or Password");
            }

            user.Token = _userMapper.GenerateAuthToken(user);

            return await Task.FromResult(user);
        }


        private IQueryable<UserDto> GetUserDtoQuery()
        {
            var usersQuery = _userRepo.GetQueyable(false);
            var countryQuery = _countryRepository.GetQueyable(false);
            var stateQuery = _stateRepository.GetQueyable(false);
            var roleQuery = _roleRepository.GetQueyable(false);
            var categoryQuery = _categoryRepository.GetQueyable(false);

            var userDtosQuery = usersQuery
                .GroupJoin(
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
                    (result, Category) =>
                    new
                    {
                        result.User,
                        result.Country,
                        result.State,
                        result.Role,
                        Category
                    }
                )
                .Select(userData => new UserDto
                {
                    Id = userData.User.Id,
                    Email = userData.User.Email,
                    Username = userData.User.Username,
                    FirstName = userData.User.FirstName,
                    LastName = userData.User.LastName,
                    PhoneNo = userData.User.PhoneNo,
                    Address1 = userData.User.Address1,
                    Address2 = userData.User.Address2,
                    CountryId = userData.User.CountryId,
                    StateId = userData.User.StateId,
                    City = userData.User.City,
                    Pincode = userData.User.Pincode,
                    Summary = userData.User.Summary,
                    BirthDate = userData.User.BirthDate,
                    AnniversaryDate = userData.User.AniversaryDate,
                    RoleId = userData.User.RoleId,
                    CategoryId = userData.User.Id,
                    AddedOn = userData.User.AddedOn,
                    Password = userData.User.Password,
                    LastUpdatedOn = userData.User.LastUpdatedOn,

                    Country = userData.Country != null ? userData.Country.CountryName : null,
                    State = userData.State != null ? userData.State.StateName : null,
                    Role = userData.Role != null ? userData.Role.RoleName : null,
                    Category = userData.Category != null ? userData.Category.CategoryName : null,
                });

            return userDtosQuery;
        }

    }

}
