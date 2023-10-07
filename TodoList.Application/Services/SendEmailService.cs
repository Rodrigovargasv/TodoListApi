using TodoList.Application.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;


namespace TodoList.Application.Services
{
    public class SendEmailService : ISendEmail
    {

        private readonly JobService _jobService;
        private readonly EmailService _emailService;

        public SendEmailService(JobService jobService, EmailService emailService)
        {
            _jobService = jobService;
            _emailService = emailService;
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
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("darktube86@gmail.com", "natl qvsk byyp nrct");

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

