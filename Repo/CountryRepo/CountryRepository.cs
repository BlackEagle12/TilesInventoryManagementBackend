
using Data.Contexts;
using Data.Models;

namespace Repo
{
    public class CountryRepository : BaseRepo<Country>, ICountryRepository
    {
        public CountryRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }
    }
}
