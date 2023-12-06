using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {

        private readonly IRecoveryPasswordUserService _recoveryPasswordUserService;

        private readonly GenerationCodeRecoveryService _generationCodeRecoveryService;


        public ForgotPasswordController(IRecoveryPasswordUserService recoveryPasswordUserService,
            GenerationCodeRecoveryService generationCodeRecoveryService)
        {
            _recoveryPasswordUserService = recoveryPasswordUserService;
            _generationCodeRecoveryService = generationCodeRecoveryService;
          
        }


        [HttpGet("/verifidEmailUser")]
        public async Task<string> ForgotPasswordUser(string email)
               => await _recoveryPasswordUserService.VerificationUserAsync(email);



        [HttpPut("/verifidCode")]
        public async Task<string> VerifidCode(string code, string password, string confirmPassword)
            => await _generationCodeRecoveryService.VerifyCodeTemporary(code, password, confirmPassword);
    }
}
