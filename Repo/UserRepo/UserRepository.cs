using Core;
using Data.Contexts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repo
{
    public class UserRepository : BaseRepo<User>, IUserRepository
    {
        public UserRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }

        private Expression<Func<User, bool>> GetExistingUserFilter(User user)
        {
            var userPradicate = PradicateBuilder.True<User>();

            return userPradicate
                    .And(x => x.Email.Equals(user.Email) ||
                        x.Username.Equals(user.Username) ||
                        x.PhoneNo.Equals(user.PhoneNo));
        }

        public async Task<bool> IsUserExistAsync(User user)
        {
            var getExistingUserFilter = GetExistingUserFilter(user);
            return await Any(getExistingUserFilter);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await GetByIdAsync(id) ?? throw new ApiException(StatusCodes.Status404NotFound, "User Not Found");
        }

        public async Task<List<User>> GetUsersPageAsync(int? pageNo, int? pageSize)
        {
            var userQueryable = GetQueyable();

            if (pageNo.HasValue && pageNo.Value > 0 && pageSize.HasValue && pageSize.Value > 0)
                userQueryable = userQueryable.Skip(pageNo.Value * pageSize.Value).Take(pageSize.Value);

            return await userQueryable.ToListAsync();
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await Any(x => x.Email.ToLower().Equals(email.ToLower()));
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            return await Any(x => x.Username.ToLower().Equals(userName.ToLower()));
        }

        public async Task<bool> IsPhoneExistAsync(string phoneNo)
        {
            return await Any(x => x.PhoneNo.ToLower().Equals(phoneNo.ToLower()));
        }

        public async Task AddUserAsync(User user)
        {
            if (await IsUserExistAsync(user))
                throw new ApiException(StatusCodes.Status400BadRequest, "User data already exist");

            user.AddedOn = DateTime.Now;
            user.LastUpdatedOn = DateTime.Now;
            await InsertAsync(user);
            await SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, User updatedUser)
        {
            var user = await GetByIdAsync(id) ?? throw new ApiException(StatusCodes.Status404NotFound, "User Not Found");

            if (await IsUserExistAsync(updatedUser))
                throw new ApiException(StatusCodes.Status400BadRequest, "User data already exist");

            user.Id = updatedUser.Id;
            user.Email = updatedUser.Email;
            user.Username = updatedUser.Username;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.PhoneNo = updatedUser.PhoneNo;
            user.Address1 = updatedUser.Address1;
            user.Address2 = updatedUser.Address2;
            user.CountryId = updatedUser.CountryId;
            user.StateId = updatedUser.StateId;
            user.City = updatedUser.City;
            user.Pincode = updatedUser.Pincode;
            user.Summary = updatedUser.Summary;
            user.BirthDate = updatedUser.BirthDate;
            user.AniversaryDate = updatedUser.AniversaryDate;
            user.RoleId = updatedUser.RoleId;
            user.CategoryId = updatedUser.Id;
            user.LastUpdatedOn = DateTime.Now;

            Update(user);
            await SaveChangesAsync();
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await GetByIdAsync(id) ?? throw new ApiException(StatusCodes.Status404NotFound, "User Not Found");
            Delete(user);
            await SaveChangesAsync();
            return user;
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> filter, bool asNoTracking = false)
        {
            var query = GetQueyable(false).Where(filter);

            return query;
        }
    }
}
