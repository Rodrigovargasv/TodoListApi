using TodoList.Application.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
namespace TodoList.Application.Services
{
    public class SendEmailService : ISendEmail
    {


        public async Task SendEmailAsync(string email, string subject, string body)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Seu Nome", "seu-email@example.com"));
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
                await client.AuthenticateAsync("darktube86@gmail.com", "wgqz pkvu zryy ojab");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }
    }

}




