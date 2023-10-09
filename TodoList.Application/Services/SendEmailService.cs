using TodoList.Application.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using TodoList.Domain.Entities;
using Microsoft.Extensions.Options;

namespace TodoList.Application.Services
{
    public class SendEmailService : ISendEmail
    {

        private readonly JobService _jobService;
        private readonly EmailService _emailService;
        private readonly EmailSetting _emailSetting;

        public SendEmailService(JobService jobService, EmailService emailService, IOptions<EmailSetting> emailSettings)
        {
            _jobService = jobService;
            _emailService = emailService;
            _emailSetting = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {

            try
            {
                var emails = _emailService.GetEmailByIdAsync(1);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("TodoListApi", "Tarefa"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = subject;

                var textPart = new TextPart("plain")
                {
                    Text = body
                };

                message.Body = textPart;
          

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSetting.SmtpServer, _emailSetting.Port, false);
                    await client.AuthenticateAsync(_emailSetting.SenderEmail, _emailSetting.Password);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }

}

