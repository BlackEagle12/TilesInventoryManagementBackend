
using Data.Models;

namespace Repo
{
    public interface IRoleRepository : IBaseRepo<Role>
    {
        Task<List<Role>> GetDefaultRolesAsync();
        Task<Role> GetRoleAsync(int roleId);
        Task<Dictionary<int, Role>> GetRoleDictAsync(List<int> roleIdList);
    }
}
