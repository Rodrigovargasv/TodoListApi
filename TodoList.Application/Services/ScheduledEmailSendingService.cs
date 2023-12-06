using eSistemCurso.Domain.Common.Exceptions;
using Hangfire;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Application.Services
{
    public class ScheduledEmailSendingService : IScheduleEmailSedingService
    {

        private readonly IEmailService _emailService;
        private readonly IJobService _jobService;

        private readonly ISendEmail _sendEmail;

        public ScheduledEmailSendingService(IEmailService emailService, IJobService jobService, ISendEmail sendEmail)
        {
            _emailService = emailService;
            _jobService = jobService;
            _sendEmail = sendEmail;
        }

        public async Task<string> ScheduleShipping(int jobId, int timeSendEmail, DataEmail emailModel)
        {

                var email = await _emailService.GetEmailByIdAsync(1);

                if (string.IsNullOrEmpty(email.Email))
                    throw new NotFoundException("Email não cadastrado.");
                

                var dateExecution = await _jobService.GetJobByIdAsync(jobId);

                var differentTime = dateExecution.ExecutionDate - DateTime.Now;

                if (differentTime.TotalSeconds <= 0)
                    throw new BadRequestException("A data de execução deve ser no futuro.");
                

                // Ajuste o atraso para enviar o email a quantidade desejada de tempo antes
                TimeSpan delay = differentTime.Subtract(TimeSpan.FromMinutes(timeSendEmail));

                BackgroundJob.Schedule(() =>
                _sendEmail.SendEmailAsync(email.Email, emailModel.Subject, emailModel.Body),
                delay);


                return "E-mail será enviado na data prevista.";
           
        }
    }
}
