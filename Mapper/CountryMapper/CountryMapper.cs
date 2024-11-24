
using Data.Models;
using Dto;

namespace Mapper
{
    public class CountryMapper
    {
        public CountryDto GetCountryDto(Country country)
        {
            return new CountryDto
            {
                Id = country.Id,
                CountryCode = country.CountryCode,
                CountryName = country.CountryName,
                Description = country.Description,
                AddedOn = country.AddedOn,
                LastUpdatedOn = country.LastUpdatedOn,
            };
        }
    }
}
