using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
