using Core;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController
            (
                IRolesService rolesService
            )
        {
            _rolesService = rolesService;
        }

        [HttpGet("GetDefault")]
        public async Task<ActionResult> GetDefault()
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _rolesService.GetDefaultRolesDDAsync()
                        )
                    );
        }

        [HttpGet]
        [TypeFilter(typeof(AuthorizationFilter))]
        public async Task<ActionResult> Get()
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _rolesService.GetAllRolesDDAsync()
                        )
                    );
        }

    }
}
