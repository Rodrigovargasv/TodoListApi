using eSistemCurso.Domain.Common.Exceptions;
using Hangfire;
using Hangfire.MemoryStorage.Database;
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

        public async Task<string> ScheduleShipping(int jobId, int timeSendEmail)
        {

            var email = await _emailService.GetEmailByIdAsync(1);

            if (string.IsNullOrEmpty(email.Email))
                throw new NotFoundException("Email não cadastrado.");


            var DataSendEmail = await _jobService.GetJobByIdAsync(jobId);

            var differentTime = DataSendEmail.ExecutionDate - DateTime.Now;

            if (differentTime.TotalSeconds <= 0)
                throw new BadRequestException("A data de execução deve ser no futuro.");

            var subject = "Tarefa a ser executada";
            var body = $"Olá, esta é uma notificação para informar que uma tarefa deverá executada em breve." + 
                $"\n\n" + $"Detalhes da tarefa:\n" + 
                $"- Nome: {DataSendEmail.Name}\n" + 
                $"- Descrição: {DataSendEmail.Description}\n" +
                $"- Data de Execução: {DataSendEmail.ExecutionDate.ToString("dd/MM/yyyy HH:mm:ss")}";



            // Ajuste o atraso para enviar o email a quantidade desejada de tempo antes
            TimeSpan delay = differentTime.Subtract(TimeSpan.FromMinutes(timeSendEmail));

            BackgroundJob.Schedule(() =>
            _sendEmail.SendEmailAsync(email.Email, subject, body),
            delay);


            return "E-mail será enviado na data prevista.";

        }
    }
}
