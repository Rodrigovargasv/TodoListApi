using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [Authorize(Roles = "commonUser, admin")]
        [HttpGet("GetEmailById/{id:int}")]
        public async Task<EmailUser> GetEmailById(int id)
            => await _emailService.GetEmailByIdAsync(id);



        [Authorize(Roles = "commonUser, admin")]
        [HttpPut("UpdateEmail/{id:int}")]
        public async Task UpdateEmail(int id, [FromBody] EmailUser email)
            => await _emailService.UpdateEmailAsync(id, email);

    }
}
