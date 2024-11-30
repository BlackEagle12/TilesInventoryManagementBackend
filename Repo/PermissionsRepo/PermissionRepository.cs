using Data.Contexts;
using Data.Models;

namespace Repo
{
    public class PermissionRepository : BaseRepo<Permission>, IPermissionRepository
    {
        public PermissionRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }
    }
}
