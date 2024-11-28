using Data.Contexts;
using Data.Models;

namespace Repo
{
    public class RolePermissionRepository : BaseRepo<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository
            (
                InventoryDBContext context
            ) : base(context)
        {
        }
    }
}
