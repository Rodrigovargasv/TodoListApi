



using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using Hangfire;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ISendEmail _sendEmail;
        private readonly IJobService _jobService;
        private readonly IEmailService _emailService;



        public SendEmailController(ISendEmail sendEmail, IJobService jobService, IEmailService emailService)
        {
            _sendEmail = sendEmail;
            _jobService = jobService;
            _emailService = emailService;



        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(int jobId, int timeSendEmail, [FromBody] DataEmail emailModel)
        {

            try
            {
                var email = _emailService.GetEmailByIdAsync(1).Result;

                if (string.IsNullOrEmpty(email.EmailSend))
                {
                    return NotFound("Email não encontrado");
                }

                var dateExecution = _jobService.GetJobByIdAsync(jobId).Result;

                var differentTime = dateExecution.ExecutionDate - DateTime.Now;

                if (differentTime.TotalSeconds <= 0)
                {
                    return BadRequest("A data de execução deve ser no futuro.");
                }

                // Ajuste o atraso para enviar o email a quantidade desejada de tempo antes
                TimeSpan delay = differentTime.Subtract(TimeSpan.FromMinutes(timeSendEmail));

                BackgroundJob.Schedule(() => 
                _sendEmail.SendEmailAsync(email.EmailSend, emailModel.Subject, emailModel.Body),
                delay);

     
                return Ok("E-mail será enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }




    }
}
