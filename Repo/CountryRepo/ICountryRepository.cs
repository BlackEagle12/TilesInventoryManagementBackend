
using Data.Models;

namespace Repo
{
    public interface ICountryRepository : IBaseRepo<Country>
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryAsync(int countryId);
        Task<Dictionary<int, Country>> GetCountryDictAsync(List<int> countryIdList);
    }
}
