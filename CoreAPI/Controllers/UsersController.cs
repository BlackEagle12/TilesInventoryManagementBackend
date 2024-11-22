using Core;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult> Get(CommonDto commonDto)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status501NotImplemented, 
                            await _userService.GetUsersPage(commonDto)
                        )
                    );
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status501NotImplemented,
                            await _userService.GetUser(id)
                        )
                    );
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status501NotImplemented,
                            await _userService.AddOrUpdateUser(userDto)
                        )
                    );
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
