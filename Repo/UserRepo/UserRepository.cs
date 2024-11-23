using Data.Contexts;
using Data.Models;

namespace Repo.UserRepo
{
    public class UserRepository : BaseRepo<User>, IUserRepository
    {
        public UserRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }
    }
}
