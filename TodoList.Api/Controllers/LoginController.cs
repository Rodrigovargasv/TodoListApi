using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Entities;
using TodoList.Infra.Data.Repository;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
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


                var token = TokenService.GenerateToken(user);

                return Ok(token);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


    }
}
