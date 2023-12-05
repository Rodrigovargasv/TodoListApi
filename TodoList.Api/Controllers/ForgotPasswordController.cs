using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {

        private readonly IRecoveryPasswordUserService _recoveryPasswordUserService;

        private readonly GenerationCodeRecoveryService _generationCodeRecoveryService;
       

        private readonly ISendEmail _sendEmail;

        private readonly IMemoryCache _memoryCache;

        private readonly IUserService _userService;


        public ForgotPasswordController(IRecoveryPasswordUserService recoveryPasswordUserService,
            GenerationCodeRecoveryService generationCodeRecoveryService, ISendEmail sendEmail, IMemoryCache memoryCache , IUserService userService)
        {
            _recoveryPasswordUserService = recoveryPasswordUserService;
            _generationCodeRecoveryService = generationCodeRecoveryService;
            _sendEmail = sendEmail;
            _memoryCache = memoryCache;
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult> ForgotPasswordUser(string email)
        {

            try
            {
                var user = await _recoveryPasswordUserService.VerificationUserAsync(email);

                if (user == null)
                    return NotFound("O email é invalido, ou não correponde ao email cadastrado para este usuário");


                await _sendEmail.SendEmailAsync(email, "Codigo de recuperação",

                    $"Segue abaixo o código de recuperação de senha:\n\n" +
                    $"Código: {_generationCodeRecoveryService.GenerationCodeTemporary()}\n\n" +
                    $"Observação: O código tem duração de 2 minutos.");

                // Armazena email em cache por 3 minutos.
                _memoryCache.Set("Email", email, TimeSpan.FromMinutes(3));


                return Ok($"Um codigo foi enviado para o email: {user.Email}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("/verifidCode")]
        public async Task<ActionResult> VerifidCode(string code, string password, string confirmPassword)
        {

            try
            {

                if (_generationCodeRecoveryService.VerifyCodeTemporary(code) is true)
                    return BadRequest("O codigo invalído ou expirado.");

                // Busca o email armazenado em cache e seta na variavel emailCache
                _memoryCache.TryGetValue("Email", out string emailCache);


                var user = await _recoveryPasswordUserService.VerificationUserAsync(emailCache);

                var userId = await _userService.GetUserByIdAsync(user.Id);


                if (password != confirmPassword)
                    return BadRequest("A Senha devem ser iguais");

                if (password is null || confirmPassword is null)
                    return BadRequest("Senha inválida");

                userId.Password = password;

                await _userService.UpdateUserAsync(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok("Senha atulizada com sucesso");

        }

    }
}
