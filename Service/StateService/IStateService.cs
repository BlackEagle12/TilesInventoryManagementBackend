
using Dto;

namespace Service
{
    public interface IStateService
    {
        Task<List<DropDownDto>> GetStatesDDAsync();
        Task<List<DropDownDto>> GetStatesDDByCountryIdAsync(int countryId);
    }
}
