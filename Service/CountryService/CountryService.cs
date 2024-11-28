
using Dto;
using Mapper;
using Repo;

namespace Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepo;
        private readonly CountryMapper _countryMapper;

        public CountryService
            (
                ICountryRepository countryRepo,
                CountryMapper countryMapper
            )
        {
            _countryRepo = countryRepo;
            _countryMapper = countryMapper;
        }

        public async Task<List<DropDownDto>> GetCountriesDDAsync()
        {
            var countries = await _countryRepo.GetAllCountriesAsync();
            return countries.Select(x => new DropDownDto() { Text = x.CountryName, Value = x.Id }).ToList();
        }
    }
}
