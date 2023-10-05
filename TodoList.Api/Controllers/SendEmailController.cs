



using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ISendEmail _sendEmail;
        private readonly IJobService _jobService;


        public SendEmailController(ISendEmail sendEmail, IJobService jobService)
        {
            _sendEmail = sendEmail;
            _jobService = jobService;



        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(int time, [FromBody] EmailModel emailModel)
        {

            try
            {

                await _sendEmail.SendEmailAsync(emailModel.ToEmail, emailModel.Subject, emailModel.Body);

                return Ok("E-mail enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }




    }
}
