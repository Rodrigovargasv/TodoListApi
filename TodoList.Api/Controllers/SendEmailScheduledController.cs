



using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using Hangfire;
using Microsoft.AspNetCore.Authorization;

namespace TodoList.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SendEmailScheduledController : ControllerBase
    {
      
        private readonly IScheduleEmailSedingService _scheduleEmailSeding;


        public SendEmailScheduledController(IScheduleEmailSedingService scheduleEmailSeding)
        {
            
            _scheduleEmailSeding = scheduleEmailSeding;
        }


        [Authorize(Roles = "commonUser, admin")]
        [HttpPost("SendEmailScheduled")]
        public async Task<string> SendEmail(int jobId, int timeSendEmail)
        
            => await _scheduleEmailSeding.ScheduleShipping(jobId, timeSendEmail);
    }
}
