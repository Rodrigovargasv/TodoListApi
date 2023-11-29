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
        [HttpPost]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(string userName, string password)
        {
            try
            {
                var user = await _userService.GetLoginAndPassWordAsync(userName, password);

                if (user is null)
                    return NotFound("Usuário ou senha inválido ");

                if (user.IsActive == false)
                    return NotFound("Usuário está inativo ou inválido");


                var token = _tokenService.GenerateToken(user);

                return Ok(token);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


    }
}
