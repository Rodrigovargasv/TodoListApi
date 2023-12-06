using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly TokenService _tokenService;

        public LoginController(IUserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(string userName, string password)
            => await _userService.GetLoginAndPassWordAsync(userName, password);
        


    }
}
