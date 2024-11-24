

using Dto;

namespace Service
{
    public interface ICountryService
    {
        Task<List<DropDownDto>> GetCountriesDDAsync();
    }
}
