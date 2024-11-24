using Core;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStateService _stateService;
        public StatesController(
                IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _stateService.GetStatesDDAsync()
                        )
                    );
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult> Get(int countryId)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _stateService.GetStatesDDByCountryIdAsync(countryId)
                        )
                    );
        }
    }
}
