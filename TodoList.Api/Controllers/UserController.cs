using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IEmailService _emailService;



        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAppUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAync();

                if (users is null)
                    return NotFound("Usuários não encontrados");

                return Ok(users);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var userId = await _userService.GetUserByIdAsync(id);

                if (userId is null)
                    return NotFound($"Usuario com id: {id} não foi encontrando");

                return Ok(userId);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUserAsync([FromBody] User user)
        {
            try
            {
                if (user is null || string.IsNullOrWhiteSpace(user.ToString()))
                    return BadRequest();

                var userId = await _userService.GetLoginAndEmailAync(user.UserName, user.Email);
        
                if (userId != null)
                   return BadRequest("Já existe um usuário com estás informações de Nome ou Email");

                await _userService.CreateUserAsync(user);
          

                return new CreatedAtRouteResult("GetUserByIdAsync", new { id = user.Id, user });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUserAsync(int id, [FromBody] User user)
        {

            var userId = await _userService.GetUserByIdAsync(id);

            var userExists = await _userService.GetLoginAndEmailAync(user.UserName, user.Email);

            if (userId is null)
                return NotFound($"Não foi encontrando o usuario com id: {id}");
            if (user is null)
                return BadRequest();

            if (userExists != null)
                return BadRequest("Não foi possível atualizar o usuário, pois já existe um usuário com estes dados");



            if (!string.IsNullOrEmpty(user.UserName))
                userId.UserName = user.UserName;

            if (!string.IsNullOrEmpty(user.Email))
                userId.Email = user.Email;


            if (!string.IsNullOrEmpty(user.Password))
                userId.Password = user.Password;

            if (!string.IsNullOrEmpty(user.Role.ToString()) && user.Role != 0)
                userId.Role = user.Role;


            try
            {
                await _userService.UpdateUserAsync(userId);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }


            return Ok(userId);

        }

        [Authorize(Roles = "admin")]

        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUserByIDAsync(int id)
        {
            var userId = await _userService.GetUserByIdAsync(id);

            if (userId is null)
                return NotFound($"Não foi encontrando o usuario com id: {id}");

            await _userService.DeleteUserAsync(userId);

            return Ok("Dados atualizado com sucesso!");
        }

    }
}
