using Core;
using Dto;
using Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LogInDto user)
        {
            return Ok(
                         new ApiResponse(
                             StatusCodes.Status200OK,
                             await _userService.LoginAsync(user.Username, user.Password)
                         )
                     );
        }
    }
}
