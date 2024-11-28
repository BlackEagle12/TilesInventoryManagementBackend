

using Core;
using Data.Contexts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await GetQueyable().ToListAsync();
        }

        public async Task<Country> GetCountryAsync(int countryId)
        {
            return await GetByIdAsync(countryId) ?? throw new ApiException(StatusCodes.Status404NotFound, "Country Not Found");
        }

        public async Task<Dictionary<int, Country>> GetCountryDictAsync(List<int> countryIdList)
        {
            return await
                        Select(x => countryIdList.Contains(x.Id))
                        .ToDictionaryAsync(x => x.Id);
        }
    }
}
