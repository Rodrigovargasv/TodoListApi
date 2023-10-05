using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using System.Threading.Tasks;



namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISendEmail _sendEmail;
        private readonly IJobService _jobService;


        public EmailController(ISendEmail sendEmail, IJobService jobService)
        {
            _sendEmail = sendEmail;
            _jobService = jobService;



        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(int time,[FromBody] EmailModel emailModel)
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

