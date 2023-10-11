using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmailById(int id) 
        {
            try
            {
                var emailId = await _emailService.GetEmailByIdAsync(id);

                if (emailId == null)
                    return BadRequest();

                if (id != emailId.Id)
                    return NotFound();

                return Ok(emailId);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmail(int id, [FromBody] Email email)
        {


            try
            {
                if (id == 1)
                    await _emailService.UpdateEmailAsync(email);
                else
                    return BadRequest();

                return Ok(email);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

    }
}
