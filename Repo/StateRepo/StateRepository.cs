
using Data.Contexts;
using Data.Models;

namespace Repo
{
    public class StateRepository : BaseRepo<State>, IStateRepoRepository
    {
        public StateRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }
    }
}
