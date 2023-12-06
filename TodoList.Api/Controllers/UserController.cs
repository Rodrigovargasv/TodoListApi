using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
           
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await _userService.GetAllUsersAync();


        [Authorize(Roles = "admin")]
        [HttpGet("GetUserById/{id:int}")]
        public async Task<User> GetUserByIdAsync(int id)
            => await _userService.GetUserByIdAsync(id);


        [Authorize(Roles = "admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUserAsync([FromBody] User user)
            => await _userService.CreateUserAsync(user);



        [Authorize(Roles = "admin")]
        [HttpPut("UpdateUser")]
        public async Task<User> UpdateUserAsync(int id, [FromBody] User user)
            => await _userService.UpdateUserAsync(user, id);


        [Authorize(Roles = "admin")]

        [HttpDelete("DeleteUser/{id:int}")]
        public async Task<string> DeleteUserByIDAsync(int id)
          => await _userService.DeleteUserAsync(id);

    }
}
