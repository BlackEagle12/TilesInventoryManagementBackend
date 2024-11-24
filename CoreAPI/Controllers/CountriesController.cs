using Core;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountriesController
            (
                ICountryService countryService
            ) 
        {
            _countryService = countryService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _countryService.GetCountriesDDAsync()
                        )
                    );
        }
    }
}
