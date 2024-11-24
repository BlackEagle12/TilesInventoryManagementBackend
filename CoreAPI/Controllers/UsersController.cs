using Core;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

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
        public async Task<ActionResult> Get(int? pageNo, int? pageSize)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK, 
                            await _userService.GetUsersPageAsync(pageNo, pageSize)
                        )
                    );
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _userService.GetUserAsync(id)
                        )
                    );
        }

        // POST api/<UsersController>/IsUserExist
        [HttpPost("IsUserExist")]
        public async Task<ActionResult> IsUserExist([FromBody] UserDto userDto)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _userService.IsUserExistAsync(userDto)
                        )
                    );
        }

        // POST api/<UsersController>/IsEmailExist
        [HttpPost("IsEmailExist")]
        public async Task<ActionResult> IsEmailExist([FromBody] string email)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _userService.IsEmailExistAsync(email)
                        )
                    );
        }

        // POST api/<UsersController>/IsUserNameExist
        [HttpPost("IsUserNameExist")]
        public async Task<ActionResult> IsUserNameExist([FromBody] string userName)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _userService.IsUserNameExistAsync(userName)
                        )
                    );
        }

        // POST api/<UsersController>/IsPhoneNoExist
        [HttpPost("IsPhoneNoExist")]
        public async Task<ActionResult> IsPhoneNoExist([FromBody] string phNo)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _userService.IsPhoneExistAsync(phNo)
                        )
                    );
        }


        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            await _userService.AddUserAsync(userDto);

            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            null,
                            "User Added sucessfully"
                        )
                    );
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserDto userDto)
        {
            await _userService.UpdateUserAsync(id, userDto);

            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            null,
                            "User updated sucessfully"
                        )
                    );
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _userService.DeleteUserAsync(id),
                            "User updated sucessfully"
                        )
                    );
        }
    }
}
