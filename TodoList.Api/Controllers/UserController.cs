using AutoMapper;
using Hangfire.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.DTOs;
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
        private readonly IMapper _mapper;


        public UserController(IUserService userService, IEmailService emailService, IMapper mapper)
        {
            _userService = userService;
            _emailService = emailService;
            _mapper = mapper;
           
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
        public async Task<ActionResult<UserDto>> UpdateUserAsync(int id, [FromBody] UserDto userDto )
        {

            var userId = await _userService.GetUserByIdAsync(id);

            if (userId is null)
                return NotFound($"Não foi encontrando o usuario com id: {id}");
            if (userDto is null)
                return BadRequest();

            if (string.IsNullOrEmpty(userDto.UserName) && string.IsNullOrEmpty(userDto.Password)
                && string.IsNullOrEmpty(userDto.Email))
                   return BadRequest("Dados inválidos.");

            userId.UpdateUser(userDto.UserName, userDto.Email, userDto.Password, userDto.IsActive);

            await _userService.UpdateUserAsync(userId);
            var updatedUserDto = _mapper.Map<UserDto>(userId);

            return Ok(updatedUserDto);

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
