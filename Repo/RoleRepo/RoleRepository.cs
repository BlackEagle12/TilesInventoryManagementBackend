
using Core;
using Data.Contexts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public class RoleRepository : BaseRepo<Role>, IRoleRepository
    {
        public RoleRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }

        public async Task<List<Role>> GetDefaultRolesAsync()
        {
            return await Select(x => x.IsDefault).ToListAsync();
        }

        public async Task<Role> GetRoleAsync(int roleId)
        {
            return await GetByIdAsync(roleId) ?? throw new ApiException(StatusCodes.Status404NotFound, "Role Not Found");
        }

        public async Task<Dictionary<int, Role>> GetRoleDictAsync(List<int> roleIdList)
        {
            return await
                        Select(x => roleIdList.Contains(x.Id))
                        .ToDictionaryAsync(x => x.Id);
        }
    }
}
